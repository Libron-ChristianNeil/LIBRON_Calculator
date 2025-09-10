namespace LIBRON_Calculator
{
    public partial class Form1 : Form
    {
        // State variables for calculations
        private double currentValue = 0;
        private double storedValue = 0;
        private string currentOperation = "";
        private bool isNewEntry = true;
        private bool operationJustPressed = false;

        public Form1()
        {
            InitializeComponent();
        }

        // Number Button Click Event Handlers
        private void AppendNumber(string number)
        {
            if (isNewEntry || textBox1.Text == "0")
            {
                textBox1.Text = number;
                isNewEntry = false;
            }
            else
            {
                textBox1.Text += number;
            }
            operationJustPressed = false;
        }

        // Handle the number buttons (0-9)
        private void button0_Click(object sender, EventArgs e) { AppendNumber("0"); }
        private void button1_Click(object sender, EventArgs e) { AppendNumber("1"); }
        private void button2_Click(object sender, EventArgs e) { AppendNumber("2"); }
        private void button3_Click(object sender, EventArgs e) { AppendNumber("3"); }
        private void button4_Click(object sender, EventArgs e) { AppendNumber("4"); }
        private void button5_Click(object sender, EventArgs e) { AppendNumber("5"); }
        private void button6_Click(object sender, EventArgs e) { AppendNumber("6"); }
        private void button7_Click(object sender, EventArgs e) { AppendNumber("7"); }
        private void button8_Click(object sender, EventArgs e) { AppendNumber("8"); }
        private void button9_Click(object sender, EventArgs e) { AppendNumber("9"); }

        private void point_Click(object sender, EventArgs e)
        {
            if (isNewEntry)
            {
                textBox1.Text = "0.";
                isNewEntry = false;
            }
            else if (!textBox1.Text.Contains("."))
            {
                textBox1.Text += ".";
            }
            operationJustPressed = false;

            //AppendNumber("0");
        }

        // Arithmetic Operation Button Handlers (+, -, x, /)
        private void PerformOperation(string operation)
        {
            // If an operation was just pressed, allow changing the operation
            if (operationJustPressed)
            {
                currentOperation = operation;
                return;
            }

            // If there's a pending operation, calculate it first
            if (!string.IsNullOrEmpty(currentOperation) && !isNewEntry)
            {
                CalculateResult();
            }

            // Store the current value and set the new operation
            if (double.TryParse(textBox1.Text, out double result))
            {
                storedValue = result;
                currentOperation = operation;
                isNewEntry = true;
                operationJustPressed = true;
            }
        }

        // Handle operation buttons
        private void add_Click(object sender, EventArgs e) { PerformOperation("+"); }
        private void subtract_Click(object sender, EventArgs e) { PerformOperation("-"); }
        private void multiply_Click(object sender, EventArgs e) { PerformOperation("x"); }
        private void divide_Click(object sender, EventArgs e) { PerformOperation("/"); }

        // Equal Button Handler
        private void equal_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentOperation) && !isNewEntry)
            {
                CalculateResult();
                currentOperation = ""; // Clear operation after calculation
            }
            operationJustPressed = false;
        }

        // Calculate the result based on the current operation
        private void CalculateResult()
        {
            if (double.TryParse(textBox1.Text, out double secondOperand))
            {
                double result = storedValue;

                switch (currentOperation)
                {
                    case "+":
                        result += secondOperand;
                        break;
                    case "-":
                        result -= secondOperand;
                        break;
                    case "x":
                        result *= secondOperand;
                        break;
                    case "/":
                        if (secondOperand != 0)
                        {
                            result /= secondOperand;
                        }
                        else
                        {
                            MessageBox.Show("Cannot divide by zero!");
                            allclear_Click(null, null);
                            return;
                        }
                        break;
                    default:
                        // No operation, just display the current value
                        result = secondOperand;
                        break;
                }

                // Format the result to avoid scientific notation for large numbers
                textBox1.Text = FormatResult(result);
                storedValue = result;
                isNewEntry = true;
            }
        }

        // Format the result to avoid scientific notation
        private string FormatResult(double result)
        {
            // If the number is too large or too small, use scientific notation
            if (Math.Abs(result) > 1e15 || (Math.Abs(result) < 1e-15 && result != 0))
            {
                return result.ToString("E10");
            }

            // Remove trailing zeros and decimal point if not needed
            string formatted = result.ToString();
            if (formatted.Contains('.'))
            {
                formatted = formatted.TrimEnd('0').TrimEnd('.');
            }

            return formatted == string.Empty ? "0" : formatted;
        }

        // Clear Button Handlers
        private void clear_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
                if (textBox1.Text == "")
                {
                    textBox1.Text = "0";
                    isNewEntry = true;
                }
            }
            operationJustPressed = false;
        }

        private void allclear_Click(object sender, EventArgs e)
        {
            // Reset everything to the initial state
            textBox1.Text = "0";
            currentValue = 0;
            storedValue = 0;
            currentOperation = "";
            isNewEntry = true;
            operationJustPressed = false;
        }

        // Decimal Point Button Handler


        // Form load event
        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "0";
        }
    }
}