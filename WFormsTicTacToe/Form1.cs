namespace WFormsTicTacToe
{
    public partial class Form1 : Form
    {
        char currentMove = 'x';
        char playerChar;
        Button[] buttons;
        Bitmap xImg = new Bitmap("x.jpg");
        Bitmap oImg = new Bitmap("o.jpg");

        public Form1()
        {
            InitializeComponent();
            buttons = new Button[] {
                button1, button2, button3,
                button4, button5, button6,
                button7, button8, button9
            };
        }

        private void DisableAllButtons(object sender, EventArgs e)
        {
            foreach (var button in buttons)
                button.Enabled = false;
        }

        private void EnableAllButtons()
        {
            foreach (var button in buttons)
                button.Enabled = true;
        }

        private void DisableOptions()
        {
            easyMode.Enabled = false;
            hardMode.Enabled = false;
            isBotFirst.Enabled = false;
        }

        private void EnableOptions()
        {
            easyMode.Enabled = true;
            hardMode.Enabled = true;
            isBotFirst.Enabled = true;
        }


        private void AddBorders()
        {
            foreach (var button in buttons)
            {
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderColor = Color.LightBlue;
                button.FlatAppearance.BorderSize = 1;
            }
        }

        private void BotsMove(char sign)
        {
            if (easyMode.Checked)
            {
                Random rand = new Random();
                List<Button> availableButtons = buttons.Where(b => b.BackgroundImage == null).ToList();
                int index = rand.Next(availableButtons.Count);

                availableButtons[index].BackgroundImage = sign == 'x' ? xImg : oImg;
                availableButtons[index].BackgroundImageLayout = ImageLayout.Stretch;
                currentMove = currentMove == 'x' ? 'o' : 'x';
                CheckForWinner();
                CheckForDraw();
            }
            else
            {
                int index = -1;
                index = TryWinningMove();
                if (index != -1)
                {
                    for (int i = 0; i < buttons.Length; i++)
                    {
                        buttons[index].BackgroundImage = playerChar == 'x' ? oImg : xImg;
                        buttons[index].BackgroundImageLayout = ImageLayout.Stretch;
                        currentMove = currentMove == 'x' ? 'o' : 'x';
                    }
                    goto CheckResult;
                    
                }

                index = TryBlockOpponent();
                if (index != -1)
                {
                    for(int i = 0; i < buttons.Length; i++)
                    {
                        buttons[index].BackgroundImage = playerChar == 'x' ? oImg : xImg;
                        buttons[index].BackgroundImageLayout = ImageLayout.Stretch;
                        currentMove = currentMove == 'x' ? 'o' : 'x'; 
                        return;
                    }
                    goto CheckResult;
                }

                if(button5.BackgroundImage == null)
                {
                    button5.BackgroundImage = playerChar == 'x' ? oImg : xImg;
                    button5.BackgroundImageLayout = ImageLayout.Stretch;
                    currentMove = currentMove == 'x' ? 'o' : 'x';
                    goto CheckResult;
                }

                Random rand = new Random();
                List<Button> availableButtons = buttons.Where(b => b.BackgroundImage == null).ToList();
                index = rand.Next(availableButtons.Count);

                availableButtons[index].BackgroundImage = sign == 'x' ? xImg : oImg;
                availableButtons[index].BackgroundImageLayout = ImageLayout.Stretch;
                currentMove = currentMove == 'x' ? 'o' : 'x';

                CheckResult:
                CheckForWinner();
                CheckForDraw();
            }
        }

        private int TryBlockOpponent()
        {
            int index;

            index = TryBlockInLine(button1, button2, button3);
            if (index != -1) return index;

            index = TryBlockInLine(button4, button5, button6);
            if (index != -1) return index;

            index = TryBlockInLine(button7, button8, button9);
            if (index != -1) return index;

            index = TryBlockInLine(button1, button4, button7);
            if (index != -1) return index;

            index = TryBlockInLine(button2, button5, button8);
            if (index != -1) return index;

            index = TryBlockInLine(button3, button6, button9);
            if (index != -1) return index;

            index = TryBlockInLine(button1, button5, button9);
            if (index != -1) return index;

            index = TryBlockInLine(button3, button5, button7);
            if (index != -1) return index;

            return index; 
        }


        private int TryBlockInLine(Button b1, Button b2, Button b3)
        {
            if ((b1.BackgroundImage != null && b1.BackgroundImage.Equals(playerChar == 'x' ? xImg : oImg)) &&
                (b2.BackgroundImage != null && b2.BackgroundImage.Equals(playerChar == 'x' ? xImg : oImg)) &&
                b3.BackgroundImage == null)
            {
                return Array.IndexOf(buttons, b3); 
            }

            if ((b1.BackgroundImage != null && b1.BackgroundImage.Equals(playerChar == 'x' ? xImg : oImg)) &&
                (b3.BackgroundImage != null && b3.BackgroundImage.Equals(playerChar == 'x' ? xImg : oImg)) &&
                b2.BackgroundImage == null)
            {
                return Array.IndexOf(buttons, b2); 
            }

            if ((b2.BackgroundImage != null && b2.BackgroundImage.Equals(playerChar == 'x' ? xImg : oImg)) &&
                (b3.BackgroundImage != null && b3.BackgroundImage.Equals(playerChar == 'x' ? xImg : oImg)) &&
                b1.BackgroundImage == null)
            {
                return Array.IndexOf(buttons, b1); 
            }

            return -1;
        }

        private int TryWinningMove()
        {
            int index = TryCompleteLine(button1, button2, button3);
            if (index != -1) return index;

            index = TryCompleteLine(button4, button5, button6);
            if (index != -1) return index;

            index = TryCompleteLine(button7, button8, button9);
            if (index != -1) return index;

            index = TryCompleteLine(button1, button4, button7);
            if (index != -1) return index;

            index = TryCompleteLine(button2, button5, button8);
            if (index != -1) return index;

            index = TryCompleteLine(button3, button6, button9);
            if (index != -1) return index;

            index = TryCompleteLine(button1, button5, button9);
            if (index != -1) return index;

            index = TryCompleteLine(button3, button5, button7);
            if (index != -1) return index;

            return -1;
        }

        private int TryCompleteLine(Button b1, Button b2, Button b3)
        {
            if ((b1.BackgroundImage != null && b1.BackgroundImage.Equals(playerChar == 'o' ? xImg : oImg)) &&
                (b2.BackgroundImage != null && b2.BackgroundImage.Equals(playerChar == 'o' ? xImg : oImg)) &&
                b3.BackgroundImage == null)
            {
                return Array.IndexOf(buttons, b3); 
            }

            if ((b1.BackgroundImage != null && b1.BackgroundImage.Equals(playerChar == 'o' ? xImg : oImg)) &&
                (b3.BackgroundImage != null && b3.BackgroundImage.Equals(playerChar == 'o' ? xImg : oImg)) &&
                b2.BackgroundImage == null)
            {
                return Array.IndexOf(buttons, b2); 
            }

            if ((b2.BackgroundImage != null && b2.BackgroundImage.Equals(playerChar == 'o' ? xImg : oImg)) &&
                (b3.BackgroundImage != null && b3.BackgroundImage.Equals(playerChar == 'o' ? xImg : oImg)) &&
                b1.BackgroundImage == null)
            {
                return Array.IndexOf(buttons, b1);  
            }

            return -1;
        }




        private bool AreImagesEqual(Button btn1, Button btn2, Button btn3)
        {
            if (btn1.BackgroundImage != null && btn2.BackgroundImage != null && btn3.BackgroundImage != null)
            {
                return btn1.BackgroundImage.Equals(btn2.BackgroundImage) && btn2.BackgroundImage.Equals(btn3.BackgroundImage);
            }
            return false;
        }

        private void DesignButtons(Button btn1, Button btn2, Button btn3)
        {
            btn1.FlatAppearance.BorderColor = btn2.FlatAppearance.BorderColor = btn3.FlatAppearance.BorderColor = Color.Red;

        }

        private bool HorizontalCheck()
        {
            if (AreImagesEqual(button1, button2, button3))
            {
                DesignButtons(button1, button2, button3);
                return true;
            }

            if (AreImagesEqual(button4, button5, button6))
            {
                DesignButtons(button4, button5, button6);
                return true;
            }

            if (AreImagesEqual(button7, button8, button9))
            {
                DesignButtons(button7, button8, button9);
                return true;
            }

            return false;
        }

        private bool VerticalCheck()
        {
            if (AreImagesEqual(button1, button4, button7))
            {
                DesignButtons(button1, button4, button7);
                return true;
            }

            if (AreImagesEqual(button2, button5, button8))
            {
                DesignButtons(button2, button5, button8);
                return true;
            }

            if (AreImagesEqual(button3, button6, button9))
            {
                DesignButtons(button3, button6, button9);
                return true;
            }

            return false;
        }

        private bool DiagonalCheck()
        {
            if (AreImagesEqual(button1, button5, button9))
            {
                DesignButtons(button1, button5, button9);
                return true;
            }

            if (AreImagesEqual(button3, button5, button7))
            {
                DesignButtons(button3, button5, button7);
                return true;
            }

            return false;
        }

        private bool IsBoardFull()
        {
            return buttons.All(b => b.BackgroundImage != null);
        }

        private bool CheckForWinner()
        {
            if (HorizontalCheck() || VerticalCheck() || DiagonalCheck())
            {
                string winner = currentMove == 'x' ? "Нолики" : "Крестики";
                MessageBox.Show($"Победили {winner}", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                foreach (var button in buttons)
                {
                    button.BackgroundImage = null;
                    button.FlatAppearance.BorderColor = Color.LightBlue;
                }

                DisableAllButtons(null, null);
                EnableOptions();
                return true;
            }

            return false;
        }

        private bool CheckForDraw()
        {
            if (IsBoardFull() && !HorizontalCheck() && !VerticalCheck() && !DiagonalCheck())
            {
                MessageBox.Show("Ничья!", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                foreach (var button in buttons)
                {
                    button.BackgroundImage = null;
                    button.FlatAppearance.BorderColor = Color.LightBlue;
                }
                DisableAllButtons(null, null); 
                EnableOptions();
                return true;
            }

            return false;
        }


        private void button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.BackgroundImage == null)
            {
                button.BackgroundImage = currentMove == 'x' ? xImg : oImg;
                button.BackgroundImageLayout = ImageLayout.Stretch;

                currentMove = currentMove == 'x' ? 'o' : 'x';

                if (CheckForWinner() || CheckForDraw())
                   return;

                if (playerChar == 'x')
                {
                    currentMove = 'o';
                    BotsMove('o');
                }
                else
                {
                    currentMove = 'x';
                    BotsMove('x');
                }

            }
            else
                MessageBox.Show("Эта клетка уже занята", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (easyMode.Checked == false && hardMode.Checked == false)
            {
                MessageBox.Show("Выберите сложность игры", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Игра началась", "Game started", MessageBoxButtons.OK, MessageBoxIcon.Information);

            EnableAllButtons();
            AddBorders();

            DisableOptions();

            playerChar = isBotFirst.Checked ? 'o' : 'x';

            currentMove = playerChar == 'x' ? 'o' : 'x'; 

            if (isBotFirst.Checked)
                BotsMove('x'); 
            else
                currentMove = 'x'; 
        }

    }
}
