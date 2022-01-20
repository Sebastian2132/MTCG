using SWE1HttpServer.app.DAL;
using SWE1HttpServer.app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE1HttpServer.app.Models
{


    public class Logger
    {
        private List<string> gameLog;

        public Logger()
        {
            this.gameLog = new List<string>();
        }
        public void GameLog(Card cardOne, Card cardTwo, int calculatedDamageOne, int calculatedDamageTwo)
        {
            string newLog = "Player";
            if (calculatedDamageOne > calculatedDamageTwo)//cardOne wins
            {
                if (cardOne.type == CardType.Monster && cardTwo.type == CardType.Monster)
                {
                    newLog += "A: " + cardOne.getName() + " (" + cardOne.Damage + " Damage) vs PlayerB: " + cardTwo.getName() + " (" + cardTwo.Damage + " Damage) => " + cardOne.getCardNameWithoutElement() + " defeats " + cardTwo.getCardNameWithoutElement();

                }
                else
                {
                    newLog += "A: " + cardOne.getName() + " (" + cardOne.Damage + " Damage) vs PlayerB: " + cardTwo.getName() + " (" + cardTwo.Damage + " Damage) => " + cardOne.Damage + " VS " + cardTwo.Damage + "-> " + calculatedDamageOne + " VS " + calculatedDamageTwo + " => " + cardOne.getName() + " wins";

                }
            }
            else if (calculatedDamageOne < calculatedDamageTwo)//cardTwo wins
            {

                if (cardOne.type == CardType.Monster && cardTwo.type == CardType.Monster)
                {
                    newLog += "B: " + cardTwo.getName() + " (" + cardTwo.Damage + " Damage) vs PlayerA: " + cardOne.getName() + " (" + cardOne.Damage + " Damage) => " + cardTwo.getCardNameWithoutElement() + " defeats " + cardOne.getCardNameWithoutElement();

                }
                else
                {
                    newLog += "B: " + cardTwo.getName() + " (" + cardTwo.Damage + " Damage) vs PlayerA: " + cardOne.getName() + " (" + cardOne.Damage + " Damage) => " + cardTwo.Damage + " VS " + cardOne.Damage + "-> " + calculatedDamageTwo + " VS " + calculatedDamageOne + " => " + cardTwo.getName() + " wins";

                }
            }
            else//draw
            {
                if (cardOne.type == CardType.Monster && cardTwo.type == CardType.Monster)
                {

                }
                else
                {

                }
                newLog += "A: " + cardOne.getName() + " (" + cardOne.Damage + " Damage) vs PlayerB: " + cardTwo.getName() + " (" + cardTwo.Damage + " Damage) => " + cardOne.Damage + " VS " + cardTwo.Damage + "-> " + calculatedDamageOne + " VS " + calculatedDamageTwo + " => Draw";

            }

            this.gameLog.Add(newLog);

        }
        public void GameLog2()
        {   
            string newLog = "";
            newLog += "Loose of cards was prevented you had luck";
            this.gameLog.Add(newLog);
        }
        public void clearLog()
        {
            this.gameLog.Clear();
        }

        public string getGameLog(string winner)
        {
            string playerLog = "Your Gamelog:\n";
            foreach (var logitem in gameLog)
            {
                playerLog += logitem + "\n";
            }
            playerLog += new string('-', 60) + "\n";
            if (winner != "D")
            {
                playerLog += "Player" + winner + " won!" + "\n";

            }
            else
            {
                playerLog += "It was a Draw!\n";

            }
            return playerLog;
        }
    }

}
