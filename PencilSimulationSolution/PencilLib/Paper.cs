using System;
using System.Collections.Generic;
using System.Text;

namespace PencilLib
{
    public class Paper
    {
        /*Constants*/


        /*Internal Private Variables */



        /*External Public Variables */

        public string Content { get; set; }

        /*Constructors*/
        public Paper()
        {

        }

        public void Prose(string testInput)
        {
            this.Content += " ";
            this.Content += testInput;
        }
    }
}
