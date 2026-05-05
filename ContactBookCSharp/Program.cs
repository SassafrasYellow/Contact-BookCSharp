namespace ContactBook;

public class Program
{
    
    public static void Main()
    {
        Contact c1= new Contact();
        Contact c2 = new Contact ("Henry");
        Contact c3 = new Contact ("Henry", "Bruckman");
        Contact c4 = new Contact ("Henry", "Bruckman", "123-456-7890");
        Contact c5 = new Contact ("Henry", "Bruckman", "123-456-7890", "henry.bruckman@example.com");
        Contact c6 = new Contact (lname: "Bruckman", email: "henry.bruckman@example.com");
    }
}
