namespace CaesarCypher;
using System;
using System.Collections.Generic;

public static class CaesarCypher
{
    private static string? caesarMessage;

    public static string? Encode(string? message, int? shift)
    {
        if (message == null)
        {
            return "INVALID INPUT";
        }
        if (shift == null)
        {
            return "INVALID SHIFT VALUE";
        }
        
        shift %= 26; // shift accounts for large numbers
        
        ResetCaesarMessage();
        for (int i = 0; i < message.Length; i++)
        {
            char currentChar = message[i];
            caesarMessage += ChangeCurrentCharacter(currentChar, (int)shift);
        }
        return caesarMessage;
    }

    public static string? Decode(string? message, int? shift)
    {
        if (message == null)
        {
            return "INVALID INPUT";
        }
        if (shift == null)
        {
            return "INVALID SHIFT VALUE";
        }
        
        shift %= 26; // shift accounts for large numbers
        
        ResetCaesarMessage();
        for (int i = 0; i < message.Length; i++)
        {
            char currentChar = message[i];
            caesarMessage += ChangeCurrentCharacter(currentChar, -1 * (int)shift);
        }
        return caesarMessage;
    }

    public static string Crack(string encodedMessage)
    {
        if (string.IsNullOrEmpty(encodedMessage))
        {
            return "INVALID INPUT";
        }

        // Dictionary of English letter frequencies (as percentages)
        Dictionary<char, double> englishFreq = new Dictionary<char, double>
        {
            {'E', 12.70}, {'T', 9.06}, {'A', 8.17}, {'O', 7.51}, {'I', 6.97},
            {'N', 6.75}, {'S', 6.33}, {'H', 6.09}, {'R', 5.99}, {'D', 4.25},
            {'L', 4.03}, {'C', 2.78}, {'U', 2.76}, {'M', 2.41}, {'W', 2.36},
            {'F', 2.23}, {'G', 2.02}, {'Y', 1.97}, {'P', 1.93}, {'B', 1.49},
            {'V', 0.98}, {'K', 0.77}, {'X', 0.15}, {'J', 0.15}, {'Q', 0.10},
            {'Z', 0.07}
        };

        double bestScore = double.MinValue;
        string bestMessage = "";

        // Try all possible shifts
        for (int shift = 0; shift < 26; shift++)
        {
            string decoded = Decode(encodedMessage, shift) ?? "";
            double score = ScoreText(decoded.ToUpper(), englishFreq);
            
            if (score > bestScore)
            {
                bestScore = score;
                bestMessage = decoded;
            }
        }

        return bestMessage;
    }

    private static void ResetCaesarMessage()
    {
        caesarMessage = "";
    }

    private static string? ChangeCurrentCharacter(char currentChar, int shift)
    {
        if (CheckForEmptyCharacter(currentChar))
        {
            return " ";
        } 
        if (!char.IsLetter(currentChar))
        {
            return currentChar.ToString();
        }
        currentChar += (char)shift;
        bool isEncode = shift > 0;
        currentChar = CheckForCharacterBound(currentChar, isEncode);
        return currentChar.ToString();
    }

    private static char CheckForCharacterBound(char currentChar, bool isEncode)
    {
        if (isEncode)
        {
            if (char.ToUpper(currentChar) < 'A')
            {
                currentChar += (char)26;
                return currentChar;
            }
            if (char.ToUpper(currentChar) > 'Z')
            {
                currentChar -= (char)26;
                return currentChar;
            }
            return currentChar;
        }
        if (char.ToLower(currentChar) < 'a')
        {
            currentChar += (char)26;
            return currentChar;
        }
        if (char.ToLower(currentChar) > 'z')
        {
            currentChar -= (char)26;
            return currentChar;
        }

        return currentChar;
    }

    private static bool CheckForEmptyCharacter(char currentChar)
    {
        if (currentChar == ' ')
        {
            return true;
        }
        return false;
    }

    private static double ScoreText(string text, Dictionary<char, double> englishFreq)
    {
        // Count letter frequencies in the text
        Dictionary<char, int> letterCount = new Dictionary<char, int>();
        int totalLetters = 0;

        foreach (char c in text)
        {
            if (char.IsLetter(c))
            {
                char upper = char.ToUpper(c);
                letterCount[upper] = letterCount.GetValueOrDefault(upper, 0) + 1;
                totalLetters++;
            }
        }

        // Calculate score based on frequency difference
        double score = 0;
        if (totalLetters == 0) return score;

        foreach (var kvp in englishFreq)
        {
            double expectedFreq = kvp.Value;
            double actualFreq = (letterCount.GetValueOrDefault(kvp.Key, 0) * 100.0) / totalLetters;
            score -= Math.Abs(expectedFreq - actualFreq);
        }

        return score;
    }
}
