namespace BankAcc
{
    public partial class Form1 : Form
    {
        private TextBox _setBalanceText;
        private TextBox _setNumberAccount;
        private TextBox _setName;
        private Button _addButton;
        private Button _clearButton;
        private Button _clearOneStrButton;
        private ListView _listView1;
        public List<BankAccount> listBankAcc = new List<BankAccount> { };

        public Form1()
        {              
            InitializeComponent();
            this.Size = new Size(800, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            //элементы формы 1
            ViewItemToForm1();
            this._addButton.Click += (sender, e) => addButtonClick();
            this._clearButton.Click += (sender, e) => clearButtonClick();
            this._clearOneStrButton.Click += (sender, e) => clearOneStrClick();
            this._listView1.MouseDoubleClick += (sender, e) => listViewDoubleClick();
        }

        //элементы формы 1
        public void ViewItemToForm1()
        {
            Label l1 = new Label()
            {
                Size = new Size(150, 20),
                Location = new Point(5, 5),
                Text = "Введите сумму"
            };
            _setBalanceText = new TextBox()
            {
                Size = new Size(170, 30),
                Location = new Point(5, l1.Height + 5)
            };
            Label l2 = new Label()
            {
                Size = new Size(170, 20),
                Location = new Point(5, l1.Height + _setBalanceText.Height + 10),
                Text = "Введите номер счета"
            };
            _setNumberAccount = new TextBox()
            {
                Size = new Size(170, 30),
                Location = new Point(5, l1.Height + _setBalanceText.Height + l2.Height + 10)
            };
            Label l3 = new Label()
            {
                Size = new Size(150, 20),
                Location = new Point(5, l1.Height + _setBalanceText.Height + l2.Height + _setNumberAccount.Height + 15),
                Text = "Введите ФИО"
            };
            _setName = new TextBox()
            {
                Size = new Size(170, 20),
                Location = new Point(5, l1.Height + _setBalanceText.Height + l2.Height + _setNumberAccount.Height + l3.Height + 15),
            };

            _addButton = new Button()
            {
                Size = new Size(170, 30),
                Location = new Point(5, l1.Height + _setBalanceText.Height + l2.Height + _setNumberAccount.Height + l3.Height + _setName.Height + 20),
                Text = "Добавить"
            };
            _clearButton = new Button()
            {
                Size = new Size(170, 30),
                Location = new Point(5, l1.Height + _setBalanceText.Height + l2.Height + _setNumberAccount.Height + l3.Height + _setName.Height + _addButton.Height + 20),
                Text = "Очистить все"
            };
            _clearOneStrButton = new Button()
            {
                Size = new Size(170, 30),
                Location = new Point(5, l1.Height + _setBalanceText.Height + l2.Height + _setNumberAccount.Height + l3.Height + _setName.Height + _addButton.Height + _clearButton.Height + 20),
                Text = "Удалить одну запись"
            };
            _listView1 = new ListView()
            {
                Size = new Size(450, 400),
                Location = new Point(_setBalanceText.Width + 10, 5),
                FullRowSelect = true
        };


            _listView1.View = View.Details;
            _listView1.Columns.Add("Номер счета", 150, HorizontalAlignment.Center);
            _listView1.Columns.Add("ФИО", 150, HorizontalAlignment.Center);
            _listView1.Columns.Add("Баланс", 100, HorizontalAlignment.Center);
            _listView1.GridLines = true;
            this.Controls.Add(l1);
            this.Controls.Add(_setBalanceText);

            this.Controls.Add(l2);
            this.Controls.Add(_setNumberAccount);

            this.Controls.Add(l3);
            this.Controls.Add(_setName);

            this.Controls.Add(_addButton);
            this.Controls.Add(_clearButton);
            this.Controls.Add(_clearOneStrButton);
            this.Controls.Add(_listView1);
        }

        public void addButtonClick()
        {

            if (!int.TryParse(_setBalanceText.Text, out int balance) || !int.TryParse(_setNumberAccount.Text, out int numberAccount))
            {
                MessageBox.Show("В поля \"Номер счета\" и \"Сумма\" необходимо ввести только целые цифры", "Внимание", MessageBoxButtons.OK);
                return;
            }
            foreach (BankAccount itemBankAccount in listBankAcc)
            {
                if (itemBankAccount.getNumberAccount() == numberAccount)
                {
                    MessageBox.Show($"Номер счета {numberAccount} уже существует", "Внимание", MessageBoxButtons.OK);
                    return;
                }
            }
            listBankAcc.Add(new BankAccount(balance, numberAccount, _setName.Text));
            string[] row = { _setNumberAccount.Text, _setName.Text, _setBalanceText.Text };
            ListViewItem item = new ListViewItem(row);
            _listView1.Items.Add(item);
        }

        public void clearButtonClick()
        {
            if (MessageBox.Show("Очистить список?", "Внимание", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) 
            { 
                _listView1.Items.Clear();
            }
            
        }

        public void clearOneStrClick()
        {
            if (_listView1.Items.Count > 0) 
            {
                foreach (ListViewItem item in _listView1.SelectedItems)
                {
                    _listView1.Items.Remove(item);
                }
            }
        }

        public void listViewDoubleClick()
        {
            string numAcc = "";
            string name = "";
            string bal = "";
            foreach (ListViewItem item in _listView1.SelectedItems)
            {
                 numAcc = item.SubItems[0].Text;
                 name = item.SubItems[1].Text;
                 bal = item.SubItems[2].Text;
            }
            FormInfo form2 = new FormInfo(numAcc, name, bal);
            form2.ShowDialog();
            foreach (ListViewItem item in _listView1.SelectedItems)
            {
                item.SubItems[2].Text = DataBuffer.intBuf.ToString();
            }

        }
    }

    public class BankAccount 
    {
        private int _balance;
        private readonly int _numberAccount;
        private string _name;
        public BankAccount(int bal, int acc, string name)
        {
            _balance = bal;
            _numberAccount = acc;
            _name = name;
        }

        public void balanceUp(int bal)
        {
            _balance += bal;    
        }
        public void balanceDown(int bal)
        {
            if (bal > _balance) 
            {
                MessageBox.Show("Невозможно совершить операция. Недостаточно средств на счете", "Ошибка", MessageBoxButtons.OK);
            } else _balance -= bal; 
        }
        public int getNumberAccount()
        {
            return _numberAccount;  
        }
    }

    public class FormInfo : Form
    {
        private Button balanceUp;
        private Button balanceDown;
        private Label l1;
        private Label l2;
        private Label l3;
        private int _balance;
        public FormInfo(string NumberAccount, string Name, string Balance)
        {
            this.Size = new Size(700, 170);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Информация";
            this.MaximizeBox = false;   
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            _balance = int.Parse(Balance);

            l1 = new Label()
            {
                Size = new Size(600, 20),
                Location = new Point(5, 5),
                Text = $"ФИО: {Name}"
            };
            l2 = new Label()
            {
                Size = new Size(600, 20),
                Location = new Point(5, l1.Height + 5),
                Text = $"Номер счета: {NumberAccount}"
            };
            l3 = new Label()
            {
                Size = new Size(600, 20),
                Location = new Point(5, l1.Height + l2.Height + 5),
                Text = $"Баланс: {Balance}"
            };
            balanceUp = new Button()
            {
                Size = new Size(170, 30),
                Location = new Point(5, l1.Height + l2.Height + l3.Height + 20),
                Text = "Пополнить баланс"
            };
            balanceDown = new Button()
            {
                Size = new Size(170, 30),
                Location = new Point(balanceUp.Width + 15, l1.Height + l2.Height + l3.Height + 20),
                Text = "Снять со счета"
            };

            this.Controls.Add(l1);
            this.Controls.Add(l2);  
            this.Controls.Add(l3);  
            this.Controls.Add(balanceUp);
            this.Controls.Add(balanceDown);

            this.balanceUp.Click += (sender, e) => balanceUpClick();
            this.balanceDown.Click += (sender, e) => balanceDownClick();
        }
        private void balanceUpClick()
        {
            DataBuffer.intBuf = _balance;
            FormUpDownBal form3 = new FormUpDownBal(1); //1 - пополнить 2 - снять
            form3.ShowDialog();
            l3.Text = $"Баланс: {DataBuffer.intBuf.ToString()}";
            _balance = DataBuffer.intBuf;
        }

        private void balanceDownClick()
        {
            DataBuffer.intBuf = _balance;
            FormUpDownBal form3 = new FormUpDownBal(2); //1 - пополнить 2 - снять
            form3.ShowDialog();
            l3.Text = $"Баланс: {DataBuffer.intBuf.ToString()}";
            _balance = DataBuffer.intBuf;
        }

    }

    public class FormUpDownBal : Form
    {
        private Button balanceOk;
        private Button balanceCancel;
        private TextBox BalanceText;
        private int _checkBtn;
        public FormUpDownBal(int checkBtn) 
        {
            _checkBtn = checkBtn;
            if (checkBtn == 1)
                this.Text = "Пополнить";
            else this.Text = "Снять";

            this.Size = new Size(200, 170);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog; 

            Label l1 = new Label()
            {
                Size = new Size(400, 20),
                Location = new Point(5, 5),
                Text = "Введите сумму"
            };
            BalanceText = new TextBox()
            {
                Size = new Size(170, 30),
                Location = new Point(5, l1.Height + 20)
            };
            balanceOk = new Button()
            {
                Size = new Size(80, 30),
                Location = new Point(5, l1.Height + 20 + BalanceText.Height + 20),
                Text = "OK"
            };
            balanceCancel = new Button()
            {
                Size = new Size(80, 30),
                Location = new Point(balanceOk.Width + 15, l1.Height + 20 + BalanceText.Height + 20),
                Text = "Отмена"
            };
            this.Controls.Add(l1);
            this.Controls.Add(BalanceText);
            this.Controls.Add(balanceOk);
            this.Controls.Add(balanceCancel);
            this.balanceCancel.Click += (sender, e) => balanceCancelClick();
            this.balanceOk.Click += (sender, e) => balanceOkClick();
        }

        public void balanceCancelClick()
        {
            this.Close();
        }

        public void balanceOkClick()
        {
            if (_checkBtn == 1)
            {
                if (!int.TryParse(BalanceText.Text, out int newsumma))
                {
                    MessageBox.Show("Yеобходимо ввести целое число", "Внимание", MessageBoxButtons.OK);
                    return;
                }
                DataBuffer.intBuf += newsumma;
                this.Close();   
            } else
            {
                if (!int.TryParse(BalanceText.Text, out int newsumma))
                {
                    MessageBox.Show("Yеобходимо ввести целое число", "Внимание", MessageBoxButtons.OK);
                    return;
                }
                if ((newsumma < 0) || (newsumma > DataBuffer.intBuf))
                {
                    MessageBox.Show("Недостаточно средств.", "Внимание", MessageBoxButtons.OK);
                    return;
                } else
                {
                    DataBuffer.intBuf -= newsumma;
                    this.Close();
                }
            }
       
        }
    }

    public static class DataBuffer
    {
        public static int intBuf = 0;
    }

}