using AirPortWebApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Xml;

namespace AirPortWebApi.Models.DataLayer
{
    public class HangerDetailsDbOperations
    {
        public static int GetLastHangerId()
        {
            AirportManagementEntities1 AE = new AirportManagementEntities1();
            var id = AE.HangerDetails.OrderByDescending(item => item.Id).Take(1).FirstOrDefault();
            if (id == null)
            {
                return 101;
            }
            else
            {
                int? no = id.Id;
                no = no + 1;
                return (int)no;
            }
        }
        public static int GetLastManagerId()
        {
            AirportManagementEntities1 AE = new AirportManagementEntities1();
            var id = AE.Managers.OrderByDescending(item => item.Id).Take(1).FirstOrDefault();
            if (id == null)
            {
                return 31;
            }
            else
            {
                int? no = id.Id;
                no = no + 1;
                return (int)no;
            }
        }
        public string AddHanger(Hanger H)
        {
            try
            {
                using(var Hd=new AirportManagementEntities1())
                {
                    var SsExists = Hd.Managers.FirstOrDefault(x => x.SSNo == H.SocialSecuirtyNo);
                    if (SsExists != null)
                    {
                        return "1,Social Security Exists";
                    }
                    else
                    {
                        var ManagerDetails = Hd.Managers.FirstOrDefault(x => x.Email == H.Email);
                        int HUniqueId = GetLastHangerId();
                        int MUniqueId = GetLastManagerId();
                        string Hid = H.HangerLocation.Substring(0, Math.Min(H.HangerLocation.Length, 3)).ToUpper() + GetLastHangerId();
                        HangerDetail h = new HangerDetail();
                        if (ManagerDetails == null)
                        {
                            var AddressId = Hd.Addresses.FirstOrDefault(x => x.HouseNo == H.HouseNo && x.City == H.City);
                            if (AddressId == null)
                            {
                                int Address_id = AddressDbOPerations.GetLastAddressId();
                                Address address = new Address();
                                address.HouseNo = H.HouseNo;
                                address.City = H.City;
                                address.Country = H.Country;
                                address.State = H.State;
                                address.PinNo = H.PinNo;
                                string AId = H.City.Substring(0, Math.Min(H.City.Length, 3)).ToUpper() + Address_id;
                                address.AddressId = AId;
                                Hd.Addresses.Add(address);
                                Hd.SaveChanges();
                                string Mid = H.SocialSecuirtyNo.Substring(H.SocialSecuirtyNo.Length - 4).ToUpper() + MUniqueId;
                                Manager m = new Manager();
                                m.ManagerId = Mid;
                                m.ManagerName= H.ManagerName;
                                m.SSNo = H.SocialSecuirtyNo;
                                m.Dob=H.Dob;
                                m.Gender= H.Gender;
                                m.Email = H.Email;
                                m.MobileNo = H.MobileNo;
                                m.AddressId = AId;
                                m.Id = MUniqueId;
                                Hd.Managers.Add(m);
                                Hd.SaveChanges();
                                h.ManagerId = Mid;
                                h.HangerId = Hid;
                                h.HangerLocation = H.HangerLocation;
                                h.HangerCapacity = H.HangerCapacity;
                                h.Id = HUniqueId;
                                Hd.HangerDetails.Add(h);
                                Hd.SaveChanges();
                                return $"0,hanger added {Hid}";
                                
                            }
                            else
                            {
                                string Mid = H.SocialSecuirtyNo.Substring(H.SocialSecuirtyNo.Length - 4).ToUpper() + MUniqueId;
                                Manager m = new Manager();
                                m.ManagerId = Mid;
                                m.ManagerName = H.ManagerName;
                                m.SSNo = H.SocialSecuirtyNo;
                                m.Dob = H.Dob;
                                m.Gender = H.Gender;
                                m.Email = H.Email;
                                m.MobileNo = H.MobileNo;
                                m.AddressId = AddressId.AddressId;
                                m.Id = MUniqueId;
                                Hd.Managers.Add(m);
                                Hd.SaveChanges();
                                h.ManagerId = Mid;
                                h.HangerId = Hid;
                                h.HangerLocation = H.HangerLocation;
                                h.HangerCapacity = H.HangerCapacity;
                                h.Id = HUniqueId;
                                Hd.HangerDetails.Add(h);
                                Hd.SaveChanges();
                                return $"0,hanger added {Hid}";

                            }
                        }
                        else
                        {
                            h.ManagerId = ManagerDetails.ManagerId;
                            h.HangerId = Hid;
                            h.HangerLocation = H.HangerLocation;
                            h.HangerCapacity = H.HangerCapacity;
                            h.Id = HUniqueId;
                            Hd.HangerDetails.Add(h);
                            Hd.SaveChanges();
                            return $"0,hanger added {Hid}";
                        }
                    }
                }
            }
            catch(DbUpdateException d)
            {
                return "1,exception";
            }
        }
    }
}