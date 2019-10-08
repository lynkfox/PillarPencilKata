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

        public void Prose(string writtenContent)
        {
            if(string.IsNullOrEmpty(this.Content) || this.Content == " ")
            {
                this.Content += writtenContent;
            }else
            {
                this.Content += " " + writtenContent;
            }
            
        }
    }
}
