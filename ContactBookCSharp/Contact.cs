namespace ContactBook;

public class Contact
{
    private string fname;
    private string lname;
    private string phone;
    private string email;

    public Contact(string fname="", string lname="", string phone="", string email="")
    {
        this.fname = fname;
        this.lname = lname;
        this.phone = phone;
        this.email = email;
    }

    public string GetFName()
    {
        return fname;
    }

    public string GetLName()
    {
        return lname;
    }

    public string GetPhone()
    {
        return phone;
    }

    public string GetEmail()
    {
        return email;
    }

    public void SetFName(string fname)
    {
        this.fname = fname;
    }

    public void SetLName(string lname)
    {
        this.lname = lname;
    }

    public void SetPhone(string phone)
    {
        this.phone = phone;
    }

    public void SetEmail(string email)
    {
        this.email = email;
    }
}
