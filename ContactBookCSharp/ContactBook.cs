namespace ContactBook;

public class ContactBook
{
    public const string YES = "Y";
    public const string NO = "N";

    public readonly string[] YES_NO = new string[] {YES, NO};

    public const string NEXT_PAGE = "+";
    public const string PREV_PAGE = "-";
    public const string GOTO_PAGE = "G";
    public const string PAGE_SIZE = "S";
    public const string CREATE_CONTACT = "C";
    public const string REVIEW_CONTACT = "R";
    public const string UPDATE_CONTACT = "U";
    public const string DELETE_CONTACT = "D";
    public const string FIND_CONTACT = "F";
    public const string ORDER_CONTACTS = "O";
    public const string DEDUPLICATE_CONTACTS = "M";
    public const string EXIT = "X";

    public readonly string[] COMMANDS = new string[]
    {
     NEXT_PAGE,
     PREV_PAGE,
     GOTO_PAGE,
     PAGE_SIZE,
     CREATE_CONTACT,
     REVIEW_CONTACT,
     UPDATE_CONTACT,
     DELETE_CONTACT,
     FIND_CONTACT,
     ORDER_CONTACTS,
     DEDUPLICATE_CONTACTS,
     EXIT
    };
    private List<Contact> allContacts;
    private int page;
    private int size;
    private bool isExit;

    public ContactBook(List<Contact> contacts = null!)
    {
        allContacts = (contacts == null) ? new List<Contact>() : contacts;
        page = 1;
        size = 10;
        isExit = false;
    }

    public void Start()
    {
        ShowWelcomeScreen();

string input;
        do
        {
            ShowContacts();
        
            do
            {
                ShowContacts();
                ShowInputOptions();
                input = GetInput();
            }
            while(!IsValidInput(input));

            ProcessInput(input);
        }
        while(!ConfirmExit());

        ShowExitScreen();
         

    }

 private void ShowWelcomeScreen()
    {
       Console.WriteLine("Welcome to Jorge's Contact Book!");
       PressEnterContinue();
    }

private void ShowContacts()
    {
        ShowContacts(allContacts,page, size);
    }

       private void ShowContacts(List<Contact> contacts, int page, int size)
    {
        Console.Clear();
       if(contacts.Count <= 0)
        {
            Console.WriteLine("No contacts to show.");
        }
        else
        {
            int indexCol = Math.Max("#".Length, contacts.Count.ToString().Length);
            int fnameCol = Math.Max("First Name".Length, contacts.Max(c => c.GetFName()?.Length ?? 0));
            int lnameCol = Math.Max("Last Name".Length, contacts.Max(c => c.GetLName()?.Length ?? 0));
            int phoneCol = Math.Max("Phone".Length, contacts.Max(c => c.GetPhone()?.Length ?? 0));
            int emailCol = Math.Max("Email".Length, contacts.Max(c => c.GetEmail()?.Length ?? 0));

            Console.WriteLine(""
                + "{0," + -indexCol + "}   "
                + "{1," + -fnameCol + "}   "
                + "{2," + -lnameCol + "}   "
                + "{3," + -phoneCol + "}   "
                + "{4," + -emailCol + "}   ",
                "#", "First Name", "Last Name", "Phone", "Email");
            Console.WriteLine(new string('~', indexCol + 2 + fnameCol + 2 + lnameCol + 2 + phoneCol + 2 + emailCol));

            int n = contacts.Count;
            int pageCount = PageCount(contacts, size);
            int s = Math.Clamp((page - 1) * size, 0, n);
            int e = Math.Clamp(s + size, 0, n);

            for (int i = s; i < e; i++)
            {
                Contact c = contacts[i];

                Console.WriteLine(""
                + "{0," + -indexCol + "}   "
                + "{1," + -fnameCol + "}   "
                + "{2," + -lnameCol + "}   "
                + "{3," + -phoneCol + "}   "
                + "{4," + -emailCol + "}   ",
                (i + 1), c.GetFName(), c.GetLName(), c.GetPhone(), c.GetEmail());
            }
            for(int i=0; i < size -e +s; i++)
            {
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine($"Page {page} of {pageCount} ({s}-{e} of {n})");
        }
    }


    private void ShowInputOptions()
    {
        string inputOptions = ""
        + $"[{NEXT_PAGE     }] Next Page        |  [{PREV_PAGE     }] Previous Page    |  [{GOTO_PAGE           }] Go to Page            |  [{PAGE_SIZE     }] Set Page Size  | \n"
        + $"[{CREATE_CONTACT}] Create Contact   |  [{REVIEW_CONTACT}] Review Contact   |  [{UPDATE_CONTACT      }] Update Contact        |  [{DELETE_CONTACT}] Delete Contact | \n"
        + $"[{FIND_CONTACT  }] Find Contact     |  [{ORDER_CONTACTS}] Order Contacts   |  [{DEDUPLICATE_CONTACTS}] Deduplicate Contacts  |  [{EXIT          }] Exit           |\n"
        + $"\n> ";
        Console.WriteLine();
        Console.WriteLine(inputOptions);
    }

    private string GetInput()
    {
        return Console.ReadLine()!.ToUpper();
    }
    
    private void ShowExitScreen()
    {
        Console.Clear();
        Console.WriteLine("Thank you for using Jorge's Contact Book. Goodbye!");
    }

    private bool ConfirmExit()
    {
      return (isExit) ? isExit = Confirm("Do you want to exit?", NO) : false;
    }

    private bool IsValidInput(string input)
    {
        if(!COMMANDS.Contains(input))
        {
            Console.WriteLine("ERROR: Invalid input. Please try again.");
            PressEnterContinue();
            return false;
        }
        else
        {
        return true;
        }
    }

     private void PressEnterContinue()
    {
        Console.WriteLine("Press ENTER to continue...");
        Console.ReadLine();
        
    }


  private void ProcessInput(string input)
    {
        switch(input)
        {
            case NEXT_PAGE: NextPage(); break;
            case PREV_PAGE: PrevPage(); break;
            case GOTO_PAGE: GotoPage(); break;
            case PAGE_SIZE: PageSize(); break;
            case CREATE_CONTACT: CreateContact(); break;
            case REVIEW_CONTACT: ReviewContact(); break;
            case UPDATE_CONTACT: UpdateContact(); break;
            case DELETE_CONTACT: DeleteContact(); break;
            case FIND_CONTACT: FindContact(); break;
            case ORDER_CONTACTS: OrderContacts(); break;
            case DEDUPLICATE_CONTACTS: DeduplicateContacts(); break;
            case EXIT: Exit(); break;
            default: break;
        }
    }

    private void NextPage()
    {
        NextPage(allContacts, ref page, size);
    }

private void NextPage(List<Contact> contacts, ref int page, int size)
    {
        page = Math.Clamp(page +1, 1, PageCount(contacts, size));
    }

  private void PrevPage()
    {
        PrevPage(allContacts, ref page, size);
    }

private void PrevPage(List<Contact> contacts, ref int page, int size)
    {
        page = Math.Clamp(page - 1, 1, PageCount(contacts, size));
    }
private void GotoPage()
    {
        GotoPage(allContacts, ref page, size);
    }
    private void GotoPage(List<Contact> contacts, ref int page, int size)
    {
        page = GetInt("Enter page", 1, PageCount(contacts, size));
    }

    private void PageSize()
    {
      PageSize(ref page,ref size);
    }

    private void PageSize(ref int page, ref int size)
    {
        int max= Console.WindowHeight - 10;
        size = GetInt("Enter page size", 1,max);
        page = 1;
    }

    private void CreateContact()
    {
        Console.Clear();
        Console.WriteLine("Create Contact");
        Console.Write(new string ('#',80));
        Console.WriteLine();
        Console.Write("First Name: ");
        string fname = Console.ReadLine()!;
        Console.Write("Last Name: ");
        string lname = Console.ReadLine()!;
        Console.Write("Phone: ");
        string phone = Console.ReadLine()!;
        Console.Write("Email: ");
        string email = Console.ReadLine()!;
        if (Confirm("Create contact with the above information?", YES))
        {
            Contact c= new Contact(fname, lname, phone, email);
            allContacts.Add(c);
            page = PageCount(allContacts, size);
             Console.WriteLine("Contact created successfully.");
             PressEnterContinue();
        }
        else
        {
            Console.WriteLine("Contact creation cancelled.");
        }
         PressEnterContinue();
    }

    private void ReviewContact()
    {
        int index= GetInt("Enter index", 1, allContacts.Count) - 1;
         Console.Clear();
        ReviewContact(index);
        PressEnterContinue();
    }
    private void ReviewContact(int index)
    {
        Contact c= allContacts[index];
        Console.WriteLine(new string('#', 80));
        Console.WriteLine("Review Contact");
        Console.WriteLine(new string('#', 80));
        Console.WriteLine();

        Console.WriteLine($"First Name: {c.GetFName()}");
        Console.WriteLine($"Last Name: {c.GetLName()}");
        Console.WriteLine($"Phone: {c.GetPhone()}");
        Console.WriteLine($"Email: {c.GetEmail()}");

        Console.WriteLine();
    }

   private void UpdateContact()
    {
        int index= GetInt("Enter index", 1, allContacts.Count) - 1;
         Console.Clear();
        UpdateContact(index);
        PressEnterContinue();
    }
    private void UpdateContact(int index)
    {
        Contact c= allContacts[index];

ReviewContact(index);
string fname= c.GetFName();
string lname= c.GetLName();
string phone= c.GetPhone(); 
string email= c.GetEmail();


        Console.WriteLine(new string('#', 80));
        Console.WriteLine("Update Contact");
        Console.WriteLine(new string('#', 80));
        Console.WriteLine();

        if(Confirm("Do you want to update the first name?", NO))
        {
            Console.Write("First Name: ");
            fname = Console.ReadLine()!;
        }
        if(Confirm("Do you want to update the last name?", NO))
        {
            Console.Write("Last Name: ");
            lname = Console.ReadLine()!;
        }
        if(Confirm("Do you want to update the phone?", NO))
        {
            Console.Write("Phone: ");
            phone = Console.ReadLine()!;
        }
        if(Confirm("Do you want to update the email?", NO))
        {
            Console.Write("Email: ");
            email = Console.ReadLine()!;
        }
        if (Confirm("Update contact with the above information?", YES))
        {
            c.SetFName(fname);
            c.SetLName(lname);
            c.SetPhone(phone);
            c.SetEmail(email);
             Console.WriteLine("Contact updated successfully.");
        }
        else
        {
            Console.WriteLine("Contact update cancelled.");
        }


        Console.WriteLine();
    }

   private void DeleteContact()
    {
        int index= GetInt("Enter index", 1, allContacts.Count) - 1;
         Console.Clear();
        RemoveContact(index);
        PressEnterContinue();
    }
    private void RemoveContact(int index)
    {
        Contact c= allContacts[index];

ReviewContact(index);
string fname= c.GetFName();
string lname= c.GetLName();
string phone= c.GetPhone(); 
string email= c.GetEmail();


        Console.WriteLine(new string('#', 80));
        Console.WriteLine("Remove Contact");
        Console.WriteLine(new string('#', 80));
        Console.WriteLine();

        if(Confirm("Do you want to remove this contact?", NO))
        {
            allContacts.RemoveAt(index);
             Console.WriteLine("Contact removed successfully.");
        }
        else
        {
            Console.WriteLine("Contact update cancelled.");
        }


        Console.WriteLine();
    }

    private void FindContact()
    {
        Console.WriteLine("Find Contact");
    }

    private void OrderContacts()
    {
        Console.WriteLine("Order Contacts");
    }

    private void DeduplicateContacts()
    {
        Console.WriteLine("Deduplicate Contacts");
    }

    private void Exit()
    {
        isExit = true;
    }

private int GetInt( string prompt, int min, int max)
    {
         string options = $"{min} - {max}";

        Console.WriteLine(prompt + $" [{options}]");
        string answer = Console.ReadLine()!.ToUpper();
        int value;

        while(!int.TryParse(answer, out value) || value < min || value > max)
        {
            Console.WriteLine("ERROR: Invalid input. Please try again.");
            Console.WriteLine(prompt + $" [{options}]");
            answer = Console.ReadLine()!;
        }
        return value;
    }

private string GetOptions(string prompt, string[] validOptions, string defaultOption)
    {
        string options = string.Join("/", validOptions);

        Console.WriteLine(prompt + $" [{options}] ({defaultOption}) ");
        string option = Console.ReadLine()!.ToUpper();

        if(string.IsNullOrWhiteSpace(option))
        {
            option = defaultOption;
        }

        while(!validOptions.Contains(option))
        {
            Console.WriteLine("ERROR: Invalid option. Please try again.");
            Console.WriteLine(prompt + $" [{options}] ({defaultOption}) ");
            option = Console.ReadLine()!.ToUpper();
        }

        return option;
    }

    private bool Confirm(string prompt, string defaultOption)
    {
        return GetOptions(prompt, YES_NO, defaultOption) == YES;
    }

   
        private static int PageCount(List<Contact> contacts, int size)
    {
        return (int)Math.Max(1, Math.Ceiling(contacts.Count / (double)size));
    }

}