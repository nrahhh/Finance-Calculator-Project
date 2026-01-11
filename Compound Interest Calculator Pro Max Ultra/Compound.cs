using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compound_Interest_Calculator_Pro_Max_Ultra
{
    public partial class Compound : Form
    {
        public Compound()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.Sizable;
            emierror_label.Visible = false;
        }

        //vars
        string currentlyChosen = "";
        string methodUsing = "";

        //
        double final_amt = 0.00;
        double principal = 0.00;
        double rate = 0.00;
        double time = 0.00;
        //emi exclusive
        double monthlypayment = 0.00;
        double annualrate = 0.00;

        //setup
        public void Setup_CI()
        {
            label2.Text = "Compound Interest";
            FinalAmt_Btn.Visible = true;
            Principal_Btn.Visible = true;
            Rate_Btn.Visible = true;
            Time_Btn.Visible = true;

            emierror_label.Visible = false;
            FinalAmt_Btn.Text = "Final Amount";
            Principal_Btn.Text = "Principal";
            Rate_Btn.Text = "Rate";
            Time_Btn.Text = "Time";
        }

        public void Setup_SI()
        {
            label2.Text = "Simple Interest";
            FinalAmt_Btn.Visible = true;
            Principal_Btn.Visible = true;
            Rate_Btn.Visible = true;
            Time_Btn.Visible = true;

            emierror_label.Visible = false;
            FinalAmt_Btn.Text = "Final Amount";
            Principal_Btn.Text = "Principal";
            Rate_Btn.Text = "Rate";
            Time_Btn.Text = "Time";
        }

        public void Setup_EG()
        {
            label2.Text = "Exponential Growth";
            FinalAmt_Btn.Visible = true;
            Principal_Btn.Visible = false;
            Rate_Btn.Visible = false;
            Time_Btn.Visible = false;

            emierror_label.Visible = false;
            FinalAmt_Btn.Text = "Growth";
            Principal_Btn.Text = "Principal";
            Rate_Btn.Text = "Rate";
            Time_Btn.Text = "Time";
        }

        public void Setup_ED()
        {
            label2.Text = "Exponential Decay";
            FinalAmt_Btn.Visible = true;
            Principal_Btn.Visible = false;
            Rate_Btn.Visible = false;
            Time_Btn.Visible = false;

            emierror_label.Visible = false;
            FinalAmt_Btn.Text = "Decay";
            Principal_Btn.Text = "Principal";
            Rate_Btn.Text = "Rate";
            Time_Btn.Text = "Time";
        }

        public void Setup_EMI()
        {
            label2.Text = "EMI";
            FinalAmt_Btn.Visible = true;
            Principal_Btn.Visible = false;
            Rate_Btn.Visible = false;
            Time_Btn.Visible = false;

            emierror_label.Visible = true;
            FinalAmt_Btn.Text = "EMI";
            Principal_Btn.Text = "Monthly Payment";
            Rate_Btn.Text = "Annual Rate";
            Time_Btn.Text = "Time";
        }

        //si methods

        public static double CalculateFinalAmountSI(double principal, double rate, double time)
        {
            double P = principal;
            double R = rate;
            double T = time;

            double si = (P * R * T) / 100;

            return principal + si;
        }

        public static double CalculatePrincipalSI(double si, double rate, double time)
        {
            double SI = si;
            double R = rate;
            double T = time;

            return (100 * SI) / (R * T);
        }

        public static double CalculateRateSI(double si, double principal, double time)
        {
            double SI = si;
            double P = principal;
            double T = time;
            
            return (100 * SI) / (P * T);
        }

        public static double CalculateTimeSI(double si, double principal, double rate)
        {
            double SI = si;
            double R = rate;
            double P = principal;

            return (100 * SI) / (P * R);
        }

        //exp growth & decay

        public static double ExpGrowth(double initial, double rate, double time)
        {
            double I = initial;
            double R = rate;
            double T = time;

            double growth = I * Math.Exp(R * T);
            return growth;
        }

        public static double ExpDecay(double initial, double rate, double time)
        {
            double I = initial;
            double R = rate;
            double T = time;

            double decay = I * Math.Exp(-R * T);
            return decay;
        }

        //ci methods
        public static double CalculateFinalAmount(double principal, double rate, double time)
        {
            double P = principal;
            double R = rate;
            double T = time;

            return P * Math.Pow((1 + R / 100), T);
        }

        public static double CalculatePrincipal(double final_amt, double rate, double time)
        {
            double A = final_amt;
            double R = rate;
            double T = time;

            return A / Math.Pow((1 + R / 100), T);
        }

        public static double CalculateRate(double final_amt, double principal, double time)
        {
            double A = final_amt;
            double P = principal;
            double T = time;

            double st1 = A / P;
            double st2 = Math.Pow(st1, 1.0 / T);
            double st3 = st2 - 1;
            double st4 = st3 * 100;

            return st4;
        }

        public static double CalculateTime(double final_amt, double principal, double rate)
        {
            double A = final_amt;
            double P = principal;
            double R = rate;

            return Math.Log(A / P) / Math.Log(1 + R / 100);
        }

        //emi method

        public double CalculateEMI(double principal, double monthlyPayment, double annualRate)
        {
            double R = annualRate / 100 / 12;
            if (monthlyPayment <= principal * R)
            {
                emierror_label.Text = "FAILED";
                return 0.00;
            }

            double months = Math.Log(monthlyPayment / (monthlyPayment - principal * R)) / Math.Log(1 + R);
            emierror_label.Text = "SUCCESS";
            return months;
        }

        //triggers
        private void Form1_Load(object sender, EventArgs e)
        {
            //init
            label2.Text = "NIL CHOSEN";
            error_disp.Text = "";
            CurrentlyChosen_Label.Text = "None Chosen";
        }

        private void FinalAmt_Btn_Click(object sender, EventArgs e)
        {
            if (methodUsing != "eg" && methodUsing != "ed" && methodUsing != "emi")
            {
                CurrentlyChosen_Label.Text = "Final Amount";
                currentlyChosen = "final_amount";
                Inp1_Label.Text = "Principal";
                Inp2_Label.Text = "Rate (Calc In %)";
                Inp3_Label.Text = "Time";
            }
            else if (methodUsing == "eg")
            {
                CurrentlyChosen_Label.Text = "Growth";
                currentlyChosen = "final_amount";
                Inp1_Label.Text = "Initial";
                Inp2_Label.Text = "Rate (Calc In %)";
                Inp3_Label.Text = "Time";
            }
            else if (methodUsing == "ed")
            {
                CurrentlyChosen_Label.Text = "Decay";
                currentlyChosen = "final_amount";
                Inp1_Label.Text = "Initial";
                Inp2_Label.Text = "Rate (Calc In %)";
                Inp3_Label.Text = "Time";
            }
            else if (methodUsing == "emi")
            {
                CurrentlyChosen_Label.Text = "EMI";
                currentlyChosen = "final_amount";
                Inp1_Label.Text = "Monthly Payment";
                Inp2_Label.Text = "Annual Rate (Calc In %)";
                Inp3_Label.Text = "Principal";
            }

        }

        private void Principal_Btn_Click(object sender, EventArgs e)
        {
            //set text
            CurrentlyChosen_Label.Text = "Principal";
            Inp1_Label.Text = "Final Amt";
            Inp2_Label.Text = "Rate (Calc In %)";
            Inp3_Label.Text = "Time";

            currentlyChosen = "principal";
        }

        private void Rate_Btn_Click(object sender, EventArgs e)
        {
            //set text
            CurrentlyChosen_Label.Text = "Rate";
            Inp1_Label.Text = "Final Amt";
            Inp2_Label.Text = "Principal";
            Inp3_Label.Text = "Time";

            currentlyChosen = "rate";
        }

        private void Time_Btn_Click(object sender, EventArgs e)
        {
            //set text
            CurrentlyChosen_Label.Text = "Time";
            Inp1_Label.Text = "Final Amt";
            Inp2_Label.Text = "Principal";
            Inp3_Label.Text = "Rate (Calc In %)";

            currentlyChosen = "time";
        }

        private void Convert_Btn_Click(object sender, EventArgs e)
        {

            if (methodUsing == "ci")
            {
                if (currentlyChosen == "final_amount")
                {
                    principal = Convert.ToDouble(Inp_1.Text);
                    rate = Convert.ToDouble(Inp_2.Text);
                    time = Convert.ToDouble(Inp_3.Text);

                    double val = CalculateFinalAmount(principal, rate, time);
                    Amount_Label.Text = val.ToString();
                }
                else if (currentlyChosen == "principal")
                {
                    final_amt = Convert.ToDouble(Inp_1.Text);
                    rate = Convert.ToDouble(Inp_2.Text);
                    time = Convert.ToDouble(Inp_3.Text);

                    double val = CalculatePrincipal(final_amt, rate, time);
                    Amount_Label.Text = val.ToString();
                }
                else if (currentlyChosen == "rate")
                {
                    final_amt = Convert.ToDouble(Inp_1.Text);
                    principal = Convert.ToDouble(Inp_2.Text);
                    time = Convert.ToDouble(Inp_3.Text);

                    double val = CalculateRate(final_amt, principal, time);
                    Amount_Label.Text = val.ToString();
                }
                else if (currentlyChosen == "time")
                {
                    final_amt = Convert.ToDouble(Inp_1.Text);
                    principal = Convert.ToDouble(Inp_2.Text);
                    rate = Convert.ToDouble(Inp_3.Text);

                    double val = CalculateTime(final_amt, principal, rate);
                    Amount_Label.Text = val.ToString();
                }
            }
            else if (methodUsing == "si")
            {
                if (currentlyChosen == "final_amount")
                {
                    principal = Convert.ToDouble(Inp_1.Text);
                    rate = Convert.ToDouble(Inp_2.Text);
                    time = Convert.ToDouble(Inp_3.Text);

                    double val = CalculateFinalAmountSI(principal, rate, time);
                    Amount_Label.Text = val.ToString();
                }
                else if (currentlyChosen == "principal")
                {
                    final_amt = Convert.ToDouble(Inp_1.Text);
                    rate = Convert.ToDouble(Inp_2.Text);
                    time = Convert.ToDouble(Inp_3.Text);

                    double val = CalculatePrincipalSI(final_amt, rate, time);
                    Amount_Label.Text = val.ToString();
                }
                else if (currentlyChosen == "rate")
                {
                    final_amt = Convert.ToDouble(Inp_1.Text);
                    principal = Convert.ToDouble(Inp_2.Text);
                    time = Convert.ToDouble(Inp_3.Text);

                    double val = CalculateRateSI(final_amt, principal, time);
                    Amount_Label.Text = val.ToString();
                }
                else if (currentlyChosen == "time")
                {
                    final_amt = Convert.ToDouble(Inp_1.Text);
                    principal = Convert.ToDouble(Inp_2.Text);
                    rate = Convert.ToDouble(Inp_3.Text);

                    double val = CalculateTimeSI(final_amt, principal, rate);
                    Amount_Label.Text = val.ToString();
                }
            }
            else if (methodUsing == "eg")
            {
                if (currentlyChosen == "final_amount")
                {
                    principal = Convert.ToDouble(Inp_1.Text);
                    rate = Convert.ToDouble(Inp_2.Text);
                    time = Convert.ToDouble(Inp_3.Text);

                    double val = ExpGrowth(principal, rate, time);
                    Amount_Label.Text = val.ToString();
                }
            }
            else if (methodUsing == "ed")
            {
                if (currentlyChosen == "final_amount")
                {
                    principal = Convert.ToDouble(Inp_1.Text);
                    rate = Convert.ToDouble(Inp_2.Text);
                    time = Convert.ToDouble(Inp_3.Text);

                    double val = ExpDecay(principal, rate, time);
                    Amount_Label.Text = val.ToString();
                }
            }
            else if (methodUsing == "emi")
            {
                if (currentlyChosen == "final_amount")
                {
                    monthlypayment = Convert.ToDouble(Inp_1.Text);
                    annualrate = Convert.ToDouble(Inp_2.Text);
                    principal = Convert.ToDouble(Inp_3.Text);

                    double val = CalculateEMI(principal, monthlypayment, annualrate);
                    Amount_Label.Text = val.ToString();
                }
            }
        }

        private void round_Click(object sender, EventArgs e)
        {
            try
            {
                if (Amount_Label.Text != "")
                {
                    double txt = Convert.ToDouble(Amount_Label.Text);
                    Amount_Label.Text = Math.Round(txt).ToString();
                    error_disp.Text = "Successful No Errors";
                }
            }
            catch (FormatException)
            {
                error_disp.Text = "FormatExeptionError";
            }
        }

        private void floor_Click(object sender, EventArgs e)
        {
            try
            {
                if (Amount_Label.Text != "")
                {
                    double txt = Convert.ToDouble(Amount_Label.Text);
                    Amount_Label.Text = Math.Floor(txt).ToString();
                    error_disp.Text = "Successful No Errors";
                }
            }
            catch (FormatException)
            {
                error_disp.Text = "FormatExeptionError";
            }
        }

        private void ceiling_Click(object sender, EventArgs e)
        {
            try
            {
                if (Amount_Label.Text != "")
                {
                    double txt = double.Parse(Amount_Label.Text, CultureInfo.InvariantCulture);
                    Amount_Label.Text = Math.Ceiling(txt).ToString();
                    error_disp.Text = "Successful No Errors";
                }
            }
            catch (FormatException)
            {
                error_disp.Text = "FormatExeptionError";
            }
        }

        private void copytoclipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Amount_Label.Text.ToString());
        }

        private void choose_si_Click(object sender, EventArgs e)
        {
            Setup_SI();
            methodUsing = "si";
        }

        private void choose_ci_Click(object sender, EventArgs e)
        {
            Setup_CI();
            methodUsing = "ci";
        }

        private void choose_eg_Click(object sender, EventArgs e)
        {
            Setup_EG();
            methodUsing = "eg";
        }

        private void choose_ed_Click(object sender, EventArgs e)
        {
            Setup_ED();
            methodUsing = "ed";
        }

        private void button1_Click(object sender, EventArgs e) // EMI Button
        {
            Setup_EMI();
            methodUsing = "emi";
        }
    }
}
