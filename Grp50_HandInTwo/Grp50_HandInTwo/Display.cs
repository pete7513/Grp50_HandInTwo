﻿
using System;

namespace Ladeskab
{
   public interface IDisplay
   {
      void ConnectPhone();
      void ReadRFID();
      void RemovePhone();
      void WrongID();
      void UnlockWithID();
      void NoConnection();
      void CurrentPowerValue(CurrentEventArgs e);
      void IndtastRFIDId();
      void PhoneOptions();
      void ShowMenu();

      void PhoneCharging();

      void PhoneConnectedMenu();
   }

   public class Display : IDisplay
   {
      private int count = 0; 

      public void ConnectPhone()
      {
         Console.WriteLine("Tilslut telefon");
      }

      public void ReadRFID()
      {
         Console.WriteLine("Indlæs RFID");
      }

      public void RemovePhone()
      {
         Console.WriteLine("Tag din telefon ud af skabet og luk døren");
      }

      public void WrongID()
      {
         Console.WriteLine("Forkert RFID tag");
      }

      public void UnlockWithID()
      {

         Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
      }

      public void NoConnection()
      {
         Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
      }

      public void CurrentPowerValue(CurrentEventArgs e)
      {
         Console.WriteLine("Current Power Value: " + e.Current);
         ++count;

         if (count == 10)
         {
            Console.Clear();
            ShowMenu();
            count = 0; 
         }
      }

      public void IndtastRFIDId()
      {
         Console.WriteLine("Indtast RFID id: ");
      }

      public void PhoneOptions()
      {
         Console.WriteLine(" [T]  Tilslut telefon   \n" +
                           " [F]  Frakoble telefon");
         Console.WriteLine(" ----------------------------------- ");

      }

      public void PhoneCharging()
      {
          Console.WriteLine(" [E]  Exit   \n" +
                            " [O]  Open door \n" +
                            " [R] RFID reader");
          Console.WriteLine(" ----------------------------------- ");
        }

      public void ShowMenu()
      {
         System.Console.WriteLine("Indtast E, O, C, R: ");


         Console.WriteLine(" [E]  Exit");
         Console.WriteLine(" [O]  Open door");
         Console.WriteLine(" [C]  Close door   \n" +
                           " [R]  RFID reader ");
         Console.WriteLine(" ----------------------------------- ");
      }

      public void PhoneConnectedMenu()
      {
          Console.WriteLine(" [C]  Close door   \n" +
                            " [F]  Frakoble telefon");
          Console.WriteLine(" ----------------------------------- ");
        }
   }
}