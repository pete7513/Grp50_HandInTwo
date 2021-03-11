using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsbSimulator;

namespace Ladeskab
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        private LadeskabState _state;
        private IUsbCharger _charger;
        private IChargeControl _chargeControl;
        private IReader _reader;
        private int _oldId;

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil
        private IDoor _door;
        private IDisplay _display;


        // Her mangler constructor

        public StationControl(IDoor door, IDisplay display, IReader reader, IChargeControl chargeControl)
        {
            _door = door;
            _display = display;
            _door.doorStatusEventHandler += _door_doorStatusEventHandler;

            _reader = reader;
            _reader.IDLoadedEvent += _reader_IDLoadedEvent;

            _chargeControl = chargeControl;
        }

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.Connected)
                    {
                        _door.LockDoor();

                        //_charger.StartCharge();
                        _chargeControl.StartCharge();

                        _oldId = id;
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
                        }

                        Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        //_charger.StopCharge();
                        _chargeControl.StartCharge();

                        _door.UnlockDoor();
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
                        }

                        _display.RemovePhone();
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.WrongID();
                    }

                    break;
            }
        }

        // Her mangler de andre trigger handlere
        private void _door_doorStatusEventHandler(object sender, CurrentDoorStatusEventArgs e)
        {
            switch (e.doorStatus)
            {
                case true:
                    _display.ConnectPhone();
                    break;
                case false:
                    _display.ReadRFID();
                    break;
            }
        }


        private void _reader_IDLoadedEvent(object sender, RfidIDEventArgs e)
        {
            e.RFIDID = _oldId;
            RfidDetected(_oldId);
        }
    }


}
