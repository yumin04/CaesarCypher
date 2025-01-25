namespace CaesarCypher_test;
using CaesarCypher;
public class CaesarCypher_test
{
    // Format for naming is "WhatWeAreTestingAndActing_InputValue_AssertionAssumption"
    [Theory]
    // [InlineData("HELLO", 3, "KHOOR")]
    // [InlineData("HELLO WORLD", 3, "KHOOR ZRUOG")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 3, "DEFGHIJKLMNOPQRSTUVWXYZABC")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", 3, "defghijklmnopqrstuvwxyzabc")]
    // [InlineData("abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ Hello World", 3, "defghijklmnopqrstuvwxyzabc DEFGHIJKLMNOPQRSTUVWXYZABC Khoor Zruog")]
    public void EncryptWord_Word_CorrectEncryption(string words, int shift, string expectedWord)
    {
        //act
        string encryptedWord = CaesarCypher.Encode(words, shift);
        
        //assert
        Assert.Equal(expectedWord, encryptedWord);
    }
    
    
    // [Theory]
    // //arrange
    // [InlineData("HELLO", 2, "KHOOR")]
    // [InlineData("HELLO ", 2, "KHOOR")]
    // public void EncryptWord_Word_IncorrectEncryption(string words, int shift, string expectedWord)
    // {
    //
    //     
    //     //act
    //     string encryptedWord = CaesarCypher_test.Encode(words, shift);
    //     
    //     //assert
    //     Assert.NotEqual(expectedWord, encryptedWord);
    // }
    
    
    [Theory]
    [InlineData("DEFGHIJKLMNOPQRSTUVWXYZABC", 3, "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("defghijklmnopqrstuvwxyzabc", 3, "abcdefghijklmnopqrstuvwxyz")]
    // [InlineData("defghijklmnopqrstuvwxyzabc DEFGHIJKLMNOPQRSTUVWXYZABC Khoor Zruog", 3, "abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ Hello World")]
    public void DecryptWord_EncryptedWord_CorrectDecryption(string words, int shift, string expectedWord)
    {
        //act
        string encryptedWord = CaesarCypher.Decode(words, shift);
        
        //assert
        Assert.Equal(expectedWord, encryptedWord);
    }
    
    
    // [Theory]
    // //arrange
    // [InlineData("KHOOR", 2, "HELLO")]
    // public void DecryptWord_EncryptedWord_IncorrectDecryption(string words, int shift, string expectedWord)
    // {
    //     //act
    //     string encryptedWord = CaesarCypher_test.Encode(words, shift);
    //     
    //     //assert
    //     Assert.Equal(expectedWord, encryptedWord);
    // }
}