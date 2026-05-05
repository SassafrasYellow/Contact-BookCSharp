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
    public ContactBook(List<Contact> contacts = null!)
    {
        allContacts = (contacts == null) ? new List<Contact>() : contacts;
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
        Console.Clear();
       if(allContacts.Count <= 0)
        {
            Console.WriteLine("No contacts to show.");
        }
        else
        {
            int indexCol = Math.Max("#".Length, allContacts.Count.ToString().Length);
            int fnameCol = Math.Max("First Name".Length, allContacts.Max(c => c.GetFName()?.Length ?? 0));
            int lnameCol = Math.Max("Last Name".Length, allContacts.Max(c => c.GetLName()?.Length ?? 0));
            int phoneCol = Math.Max("Phone".Length, allContacts.Max(c => c.GetPhone()?.Length ?? 0));
            int emailCol = Math.Max("Email".Length, allContacts.Max(c => c.GetEmail()?.Length ?? 0));

            Console.WriteLine(""
                + "{0," + -indexCol + "}   "
                + "{1," + -fnameCol + "}   "
                + "{2," + -lnameCol + "}   "
                + "{3," + -phoneCol + "}   "
                + "{4," + -emailCol + "}   ",
                "#", "First Name", "Last Name", "Phone", "Email");
                Console.WriteLine(new string('~',indexCol + 2 + fnameCol + 2 + lnameCol + 2 + phoneCol + 2 + emailCol));

            int n = allContacts.Count;
            int page = 1;
            int size = 10;
            int pageCount = (int) Math.Max(1, Math.Ceiling(n/ (double) size));
            int s = Math.Clamp((page-1) * size, 0, n);
            int e = Math.Clamp(s + size, 0, n);

            for(int i = s; i < e; i++)
            {
                Contact c = allContacts[i];

                Console.WriteLine(""
                + "{0," + -indexCol + "}   "
                + "{1," + -fnameCol + "}   "
                + "{2," + -lnameCol + "}   "
                + "{3," + -phoneCol + "}   "
                + "{4," + -emailCol + "}   ",
                (i+1), c.GetFName(), c.GetLName(), c.GetPhone(), c.GetEmail());
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
        
    }

    private bool ConfirmExit()
    {
      return Confirm("Do you want to exit?", NO);
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
        Console.WriteLine("Next Page");
    }

    private void PrevPage()
    {
        Console.WriteLine("Previous Page");

    }

    private void GotoPage()
    {
        Console.WriteLine("Go to Page");
    }

    private void PageSize()
    {
        Console.WriteLine("Set Page Size");
    }

    private void CreateContact()
    {
        Console.WriteLine("Create Contact");
    }

    private void ReviewContact()
    {
        Console.WriteLine("Review Contact");
    }

    private void UpdateContact()
    {
        Console.WriteLine("Update Contact");
    }

    private void DeleteContact()
    {
        Console.WriteLine("Delete Contact");
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
        Console.WriteLine("Exiting...");
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

    private void PressEnterContinue()
    {
        Console.WriteLine("Press ENTER to continue...");
        Console.ReadLine();
        
    }
}