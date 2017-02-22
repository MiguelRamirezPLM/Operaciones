using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace PLMLicenseComponent
{
    public class LicenseComponent
    {
        #region Constructors

        public LicenseComponent() { }

        #endregion

        #region Public Methods

        public string generateLicense()
        {
            return this.generateString(16);

        }

        public string generateCode(int counter)
        {
            return this.generateString(counter);
        }
           
        #endregion

        #region Private Methods

        private string generateString(int counter)
        {
            string key = "";

            int z = 0;

            while (z < counter)
            {
                char character;

                character = this.getCharacter(this._rand.Next(100));

                if (character != 'O' && character != '0' && character != 'Ñ')
                {
                    if (key.Length > 0)
                    {
                        if (character != key[z - 1])
                        {
                            key = key + character;

                            z++;
                        }
                    }
                    else
                    {
                        key = key + character;
                        z++;
                    }

                }
            }

            return key;
        }

        private char getCharacter(int number)
        {
            char ch;

            if (number < 50)
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(10 * _rand.NextDouble() + 48)));
            else
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * _rand.NextDouble() + 65)));

            return ch;
        }

        #endregion


        private Random _rand = new Random();

    }
}
