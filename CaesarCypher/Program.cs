using static CaesarCypher.CaesarCypher;

class Program  
{  
    static void Main(string[] args)  
    {  
        while (true)  
        {  
            Console.Write("Enter a string to encode (or type 'quit' to exit): "); 
            string? input = Console.ReadLine();  
  
            if (input.Equals("quit", StringComparison.OrdinalIgnoreCase))  
            {  
                Console.WriteLine("Goodbye!");  
                break;  
            }  
  
            int shift;  
            while (true)  
            {  
                Console.Write("Enter a shift number (integer): ");  
                string shiftInput = Console.ReadLine();  
  
                if (int.TryParse(shiftInput, out shift))  
                    break;   
                else  
                    Console.WriteLine("Please enter a valid integer.");  
            }
            
            // Somehow it says "cannot resolve symbol Encode"
            string? encoded = Encode(input, shift); 
            Console.WriteLine($"Encoded string: {encoded}");  
            string? decoded = Decode(encoded, shift);  
            Console.WriteLine($"Decoded string: {decoded}");  
        }  
    }  
}