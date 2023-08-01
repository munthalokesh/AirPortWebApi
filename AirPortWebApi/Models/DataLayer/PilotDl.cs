using AirPortWebApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AirPortWebApi.Models.DataLayer
{
    public class PilotDl
    {
        public string AddingPilot(PilotCls p)
        {
            try
            {
                using(var pilot=new AirportManagementEntities())
                {
                    Address address = new Address();
                    address.HouseNo=p.HouseNo;
                    address.City=p.City;
                    address.Country=p.Country;
                    address.State=p.State;
                    address.PinNo=p.PinNo;
                    string AddressId = pilot.Addresses.FirstOrDefault(x => x.HouseNo == p.HouseNo && x.City == p.City).AddressId;
                    if (AddressId == null)
                    {
                        pilot.Addresses.Add(address);
                        pilot.SaveChanges();
                        AddressId = pilot.Addresses.FirstOrDefault(x => x.HouseNo == p.HouseNo && x.City == p.City).AddressId;
                        Pilot Pobj = new Pilot();
                        Pobj.AddressId = AddressId;
                        Pobj.PilotName = p.PilotName;
                        Pobj.LicenceNo = p.LicenseNo;
                        Pobj.SSNo = p.SocialSecurityNo;
                        Pobj.Dob = p.DateOfBirth;
                        Pobj.Gender = p.Gender;
                        Pobj.Email = p.EmailAddress;
                        Pobj.MobileNo = p.MobileNo;
                        pilot.Pilots.Add(Pobj);
                        pilot.SaveChanges();
                    }
                    else
                    {
                        AddressId = pilot.Addresses.FirstOrDefault(x => x.HouseNo == p.HouseNo && x.City == p.City).AddressId;
                        Pilot Pobj= new Pilot();
                        Pobj.PilotId = "p001";
                        Pobj.AddressId = AddressId;
                        Pobj.PilotName=p.PilotName;
                        Pobj.LicenceNo = p.LicenseNo;
                        Pobj.SSNo = p.SocialSecurityNo;
                        Pobj.Dob = p.DateOfBirth;
                        Pobj.Gender= p.Gender;
                        Pobj.Email = p.EmailAddress;
                        Pobj.MobileNo= p.MobileNo;
                        pilot.Pilots.Add(Pobj);
                        pilot.Entry(Pobj).State=System.Data.Entity.EntityState.Added;
                        pilot.SaveChanges();
                    }
                    return "pilot added";
                }
            }
            catch(DbUpdateException d)
            {
                SqlException s = d.GetBaseException() as SqlException;
                return s.ToString();
            }
        }
    }
}