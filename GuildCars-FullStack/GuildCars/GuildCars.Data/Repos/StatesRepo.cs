﻿using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Repos
{
    public class StatesRepo : IStatesRepo
    {
        public List<State> GetAll()
        {
            List<State> states = new List<State>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("StatesSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        State currentRow = new State();
                        currentRow.StateId = dr["StateId"].ToString();
                        currentRow.StateName = dr["StateName"].ToString();

                        states.Add(currentRow);
                    }
                }
            }
            return states;
        }
    }
}
