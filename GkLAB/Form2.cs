using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GkLAB
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            
            richTextBox1.ReadOnly = true;
            String instruction =
                "Instrukcja obsługi!\n\n" +
                "Aktywna bila to bila położona po prawej stronie stołu bez innych bill\n" +
                "Przycisk W-poruszenie aktywna bilą w dodatnim kierunku osi x.\n" +
                "Przycisk A-poruszenie aktywna bilą w dodatnim kierunku osi y.\n" +
                "Przycisk S-poruszenie aktywna bilą w ujemnym kierunku osi x.\n" +
                "Przycisk D-poruszenie aktywna bilą w ujemnym kierunku osi y.\n" +
                "Przycisk E i Q-obrót aktywnej bili wokół jej osi symetri prostopadłej do stołu.\n\n" +
                "Pole Camera zawiera trzy tryby przełaczale kamery:\n" +
                "\t Round Table - widok na cały stół.\n" +
                "\t Above Ball - widok na aktywna bile z góry.\n" +
                "\t From Ball - widok z bili tak jakby obserwator na niej siedział. \n\n" +
                "Pole Light zawiera dwa tryby z którego dochodzi światło:\n" +
                "\t Up Light - światła z lampki która znajduje się nad stołem.\n" +
                "\t On Ball - światło znajduje sie na szczycie aktywnej bili.\n" +
                "Pola z parametrami światła - po wprowadzeniu zmina nalezy zatwierdzić klikając przycisk Apply.\n" +
                "Pola Shading : dostepny jeden z trzech mozliwych cieniowań wystarczy zaznaczyć odpowiednie cieniowanie i zmieni się ono automatycznie.\n" +
                "Pole Active Ball: mozna wprowadzić krok o jaki pszczemieszcza i obraca sie aktywna billa.\n" +
                "Po zmianie nalezy zatwierdzić przyciskiem Apply.";

          
                richTextBox1.AppendText(instruction);
            


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
