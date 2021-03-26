//-----------------------------------------------------------------------
// <copyright file=" ServerState.cs" company="HaotianCloud">
// * Copyright (C) HaotianCloud.Framework  All Rights Reserved
// * version : 1.0
// * author  : HaotianCloud.Framework
// * FileName: ServerState.cs
// * history : Created by T4 04/13/2020 11:54:49 
// </copyright>
//-----------------------------------------------------------------------
using System;
using Chloe.Annotations;

namespace HaotianCloud.Domain.SystemSecurity
{
	/// <summary>
	/// ServerState Entity Model
	/// </summary>
	[TableAttribute("sys_serverstate")]
    public class ServerStateEntity : IEntity<ServerStateEntity>
    {
						[ColumnAttribute("F_Id", IsPrimaryKey = true)]
			public  String  F_Id { get; set; }
					
			public  String  F_WebSite { get; set; }
					
			public  String  F_ARM { get; set; }
					
			public  String  F_CPU { get; set; }
					
			public  String  F_IIS { get; set; }
					
			public  DateTime  F_Date { get; set; }
		public int F_Cout { get; set; }
	}
}