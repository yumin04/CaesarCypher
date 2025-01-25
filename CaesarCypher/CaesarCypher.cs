namespace CaesarCypher;

public static class CaesarCypher
{
    private static string caesarMessage;

    public static string Encode(string message, int shift)
    {
        ResetCaesarMessage();
        for (int i = 0; i < message.Length; i++)
        {
            char currentChar = message[i];
            caesarMessage += ChangeCurrentCharacter(currentChar, shift);
        }
        return caesarMessage;
    }

    public static string Decode(string message, int shift)
    {
        ResetCaesarMessage();
        for (int i = 0; i < message.Length; i++)
        {
            char currentChar = message[i];
            caesarMessage += ChangeCurrentCharacter(currentChar, -1 * shift);
        }
        return caesarMessage;
    }
    private static void ResetCaesarMessage()
    {
        caesarMessage = "";
    }
    private static string ChangeCurrentCharacter(char currentChar, int shift)
    {
        if (CheckForEmptyCharacter(currentChar))
        {
            return " ";
        }
        currentChar += (char)shift;
        currentChar = CheckForCharacterBound(currentChar);
        return currentChar.ToString();
    }

    private static char CheckForCharacterBound(char currentChar)
    {
        if (currentChar < 'A')
        {
            currentChar += (char)26;
            return currentChar;
        }
        if (currentChar > 'z')
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
}