using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Alice_server
{
    static class Command
    {
        public static void _start_command(byte[] command, IPEndPoint remoteEP, AllUser AllPlayer, AllPrey AllPrey)
        {
            string str_command = BaseTools.Convertbtst(command);
            string[] mas_com = str_command.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);


            switch (mas_com[0])
            {
                case "ping_1":
                    _for_ping(remoteEP);
                    break;
                case "0":
                    _for_new_user(AllPlayer, remoteEP);
                    break;
                case "prey_get_token":
                    _RegistrNewPrey(mas_com, remoteEP, AllPrey);
                    break;
                case "prey_online":
                    _ForPreyOnline(mas_com, remoteEP, AllPrey);
                    break;

                case "append_prey":
                    _AppendPrey(mas_com, remoteEP,AllPlayer, AllPrey);
                    break;
                case "need_prey":
                    _NeedPrey(mas_com, remoteEP, AllPrey);
                    break;
                default:
                    _OtherCommand(AllPlayer, remoteEP, mas_com);
                    break;
            }
        }

        private static void _for_ping(IPEndPoint remoteEP)
        {
            Console.WriteLine(remoteEP.ToString() + " " + ReceiverUDP.udpServer.Client.LocalEndPoint.ToString() + "  ");
            byte[] msg = Encoding.ASCII.GetBytes(remoteEP.ToString());
            Console.WriteLine("pong_1");
            ReceiverUDP.udpServer.Send(msg, msg.Length, remoteEP);
        }

        public static void _for_new_user(AllUser AllPlayer, IPEndPoint remoteEP)
        {
            int id = AllPlayer._new_user();
            byte[] msg = Encoding.ASCII.GetBytes(AllPlayer.AllUser_database[id - 1].Authorization());
            ReceiverUDP.udpServer.Send(msg, msg.Length, remoteEP);
        }

        public static void _OtherCommand(AllUser AllPlayer, IPEndPoint remoteEP, string[] mas)
        {
            UserClient _client = new UserClient(mas[2], mas[1], Convert.ToInt32(mas[0]));

            switch (mas[3])
            {
                case "get_all_sacrifice":
                    _SearchUserInBase(AllPlayer, remoteEP, ref _client);
                    break;

                default:
                    Console.WriteLine(string.Join("", mas));
                    break;

            }
        }

        public static void _SearchUserInBase(AllUser AllPlayer, IPEndPoint remoteEP, ref UserClient _client)
        {
            foreach (var _user in AllPlayer.AllUser_database)
            {
                if (_CompareUser(_user, _client))
                {
                    _user._now_ip_port = remoteEP;
                    _client._now_ip_port = remoteEP;
                    _client._all_sacrifice = _user._all_sacrifice;
                    _client.online = true;
                    string allSacrifice = "[get_all_sacrifice]" + _client._AllSacrificeToString();
                    byte[] com = BaseTools.Convertbtst(allSacrifice);
                    ReceiverUDP.udpServer.Send(com, com.Length, remoteEP);
                }
            }

        }

        private static bool _CompareUser(UserClient _baseuser, UserClient _client)
        {
            if (_baseuser._Login == _client._Login && _baseuser._cookies == _client._cookies && _baseuser._ID_USER == _client._ID_USER)
                return true;
            else
                return false;
        }
        private static bool _ComparePrey(Prey _baseuser, Prey _client)
        {
            if (_baseuser.Name_sacrifice == _client.Name_sacrifice && _baseuser.Token_sacrifice == _client.Token_sacrifice)
                return true;
            else
                return false;
        }

        private static void _RegistrNewPrey(string[] str_com, IPEndPoint remoteEP, AllPrey AllPrey)
        {
            Prey _prey = new Prey(remoteEP.Address.ToString(), remoteEP.Port, str_com[1], BaseTools._GenerateTokenPrey());
            AllPrey.AllPrey_database.Add(_prey);
            byte[] camm = BaseTools.Convertbtst("[token][" + _prey.Token_sacrifice + "]");
            ReceiverUDP.udpServer.Send(camm, camm.Length, remoteEP);
            Console.WriteLine(remoteEP.Address + ":" + remoteEP.Port + "присвоен токен " + _prey.Token_sacrifice);
            AllPrey.Save(_prey.Name_sacrifice, _prey.Token_sacrifice, remoteEP.Address.ToString(), remoteEP.Port.ToString());
        }

        private static void _ForPreyOnline(string[] str_com, IPEndPoint remoteEP, AllPrey AllPrey)
        {
            Prey _online = new Prey(remoteEP.Address.ToString(), remoteEP.Port, str_com[1], str_com[2]);
            foreach (var _prey in AllPrey.AllPrey_database)
            {
                if (_ComparePrey(_prey, _online))
                {
                    _prey.online = true;
                    _prey.ip = remoteEP;
                    Console.WriteLine(_prey.ip.Address + ":" + _prey.ip.Port + "  " + _prey.Name_sacrifice + "  online");
                }
            }
        }

        private static void _AppendPrey(string[] str_com, IPEndPoint remoteEP, AllUser AllPlayer, AllPrey AllPrey)
        {
            UserClient _one_user = new UserClient(str_com[4], str_com[5], Convert.ToInt32(str_com[3]));
            Prey _one_prey = new Prey(str_com[1], str_com[2]);
            bool notfound = false;
            
            foreach (var _user in AllPlayer.AllUser_database)
                if (_CompareUser(_user, _one_user))
                {
                    foreach (var _prey in AllPrey.AllPrey_database)
                        if (_ComparePrey(_prey, _one_prey))
                        {
                            _user._all_sacrifice.Add(_prey);
                            Console.WriteLine("[append_prey][OK]");
                            byte[] msg = BaseTools.Convertbtst("[append_prey][OK]");
                            ReceiverUDP.udpServer.Send(msg,msg.Length, remoteEP);
                            AllPlayer._SavePrey(_prey, _user);
                            notfound = true;
                            break; 
                        }

                    break;
                }
            if (!notfound)
            {
                byte[] btnotfound = BaseTools.Convertbtst("[append_prey][not found]");
                ReceiverUDP.udpServer.Send(btnotfound, btnotfound.Length, remoteEP);
            }
           
        }
        private static void _NeedPrey(string[] str_com, IPEndPoint remoteEP, AllPrey AllPrey)
        {
            Prey cheackPrey = new Prey(str_com[1], str_com[2]);
            foreach (var prey in AllPrey.AllPrey_database)
            {
                if(_ComparePrey(prey,cheackPrey))
                {
                    
                        string cmdforclient = "[for_prey_connect]["+ prey.online + "][" + prey.ip + "]";
                    Console.WriteLine(cmdforclient);
                        byte[] bt= BaseTools.Convertbtst(cmdforclient);
                        ReceiverUDP.udpServer.Send(bt, bt.Length, remoteEP);

                        string cmdforsrv = "[for_prey_connect][" +remoteEP+"]";
                    Console.WriteLine(cmdforsrv);
                        byte[] btsrv = BaseTools.Convertbtst(cmdforsrv);
                        ReceiverUDP.udpServer.Send(btsrv, btsrv.Length, prey.ip);
                    
                }

            }

        }
    }
}
