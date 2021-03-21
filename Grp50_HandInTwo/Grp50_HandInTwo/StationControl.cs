using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        public enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        public LadeskabState _state;
        //private IUsbCharger _charger;
        private IChargeControl _chargeControl;
        private ILog _log;
        private IReader _reader;
        public int _oldId;

        //private string logFile = "logfile.txt"; // Navnet på systemets log-fil
        private IDoor _door;
        private IDisplay _display;


        // Her mangler constructor

        public StationControl(IDoor door, IDisplay display, IReader reader, IChargeControl chargeControl, ILog log)
        {
            _door = door;
            _display = display;
            _door.doorStatusEventHandler += _door_doorStatusEventHandler;

            _reader = reader;
            _reader.IDLoadedEvent += _reader_IDLoadedEvent;

            _chargeControl = chargeControl;
            _log = log;
        }

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_chargeControl.Connected)
                    {
                        _door.LockDoor();

                        //_charger.StartCharge();
                        _chargeControl.StartCharge();

                        _oldId = id;

                        _log.LockwriteToFile(id);
          
                        _display.UnlockWithID();

                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                       _display.NoConnection();
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                       _oldId = id;

                        //_charger.StopCharge();
                        _chargeControl.StopCharge();

                        _door.UnlockDoor();
                        
                        _log.UnlockWriteToFile(id);

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
            switch (e.IsDoorOpen_Status)
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
            //e.RFIDID = _oldId;
            RfidDetected(e.RFIDID);
        }
    }


}
