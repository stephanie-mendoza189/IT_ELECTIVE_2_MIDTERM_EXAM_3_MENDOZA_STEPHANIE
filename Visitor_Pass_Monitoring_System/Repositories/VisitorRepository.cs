using System;
using System.Collections.Generic;
using Visitor_Pass_Monitoring_System.Models;

namespace Visitor_Pass_Monitoring_System.Repositories
{
    public class VisitorRepository
    {
        private static List<Visitor> _visitors = new List<Visitor>();
        private static int _nextId = 1;

        public List<Visitor> GetAll()
        {
            return _visitors;
        }

        public Visitor GetById(int id)
        {
            foreach (Visitor v in _visitors)
            {
                if (v.Id == id)
                {
                    return v;
                }
            }
            return null;
        }

        public void Add(Visitor visitor)
        {
            visitor.Id = _nextId;
            visitor.PassNumber = "PASS-" + _nextId.ToString("D4");
            visitor.EntryDateTime = DateTime.Now;
            visitor.Status = "Inside Building";

            _nextId = _nextId + 1;
            _visitors.Add(visitor);
        }

        public void Update(Visitor updatedVisitor)
        {
            for (int i = 0; i < _visitors.Count; i++)
            {
                if (_visitors[i].Id == updatedVisitor.Id)
                {
                    _visitors[i].FirstName = updatedVisitor.FirstName;
                    _visitors[i].LastName = updatedVisitor.LastName;
                    _visitors[i].Company = updatedVisitor.Company;
                    _visitors[i].ContactNumber = updatedVisitor.ContactNumber;
                    _visitors[i].PersonToVisit = updatedVisitor.PersonToVisit;
                    _visitors[i].Department = updatedVisitor.Department;
                    _visitors[i].Purpose = updatedVisitor.Purpose;
                    _visitors[i].ValidIdPresented = updatedVisitor.ValidIdPresented;
                    _visitors[i].Notes = updatedVisitor.Notes;
                }
            }
        }

        public void RecordExit(int id)
        {
            foreach (Visitor v in _visitors)
            {
                if (v.Id == id)
                {
                    v.ExitDateTime = DateTime.Now;
                    v.Status = "Left Building";
                }
            }
        }
    }
}