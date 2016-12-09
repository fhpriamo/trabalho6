using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace trabalho6.comum
{
    public class Console : Escrevivel
    {
        private TextBox console;

        public Console(TextBox controle)
        {
            console = controle;
        }

        public void Escreve(string mensagem)
        {
            if (console.InvokeRequired)
            {
                console.BeginInvoke(new Action<string>(this.EscreveNoTextBox), mensagem);
            }
            else
            {
                EscreveNoTextBox(mensagem);
            }
        }

        public void Sobrescreve(string mensagem)
        {
            Apaga();
            Escreve(mensagem);
        }

        public void Apaga()
        {
            if (console.InvokeRequired)
            {
                console.BeginInvoke(new Action(this.ApagaOTextBox));
            }
            else
            {
                ApagaOTextBox();
            }
        }

        public void ApagaOTextBox()
        {
            console.Clear();
        }

        public void EscreveNoTextBox(string mensagem)
        {
            console.AppendText(mensagem + Environment.NewLine); 
        }
    }
}
