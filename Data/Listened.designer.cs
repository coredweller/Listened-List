﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.261
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data.Repository
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Listened")]
	public partial class Database : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertShow(Data.DomainObjects.Show instance);
    partial void UpdateShow(Data.DomainObjects.Show instance);
    partial void DeleteShow(Data.DomainObjects.Show instance);
    partial void InsertListenedShow(Data.DomainObjects.ListenedShow instance);
    partial void UpdateListenedShow(Data.DomainObjects.ListenedShow instance);
    partial void DeleteListenedShow(Data.DomainObjects.ListenedShow instance);
    partial void InsertTag(Data.DomainObjects.Tag instance);
    partial void UpdateTag(Data.DomainObjects.Tag instance);
    partial void DeleteTag(Data.DomainObjects.Tag instance);
    #endregion
		
		public Database() : 
				base(global::Data.Properties.Settings.Default.ListenedConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public Database(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public Database(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public Database(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public Database(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Data.DomainObjects.Show> Shows
		{
			get
			{
				return this.GetTable<Data.DomainObjects.Show>();
			}
		}
		
		public System.Data.Linq.Table<Data.DomainObjects.ListenedShow> ListenedShows
		{
			get
			{
				return this.GetTable<Data.DomainObjects.ListenedShow>();
			}
		}
		
		public System.Data.Linq.Table<Data.DomainObjects.Tag> Tags
		{
			get
			{
				return this.GetTable<Data.DomainObjects.Tag>();
			}
		}
	}
}
namespace Data.DomainObjects
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Show")]
	public partial class Show : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _ShowId;
		
		private string _VenueName;
		
		private string _City;
		
		private string _State;
		
		private string _Country;
		
		private System.Nullable<System.DateTime> _ShowDate;
		
		private string _Notes;
		
		private System.DateTime _CreatedDate;
		
		private System.Nullable<System.DateTime> _UpdatedDate;
		
		private System.Nullable<System.DateTime> _DeletedDate;
		
		private bool _Deleted;
		
		private string _VenueNotes;
		
		private string _PhishNetUrl;
		
		private EntitySet<ListenedShow> _ListenedShows;
		
		private EntitySet<Tag> _Tags;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(System.Guid value);
    partial void OnIdChanged();
    partial void OnVenueNameChanging(string value);
    partial void OnVenueNameChanged();
    partial void OnCityChanging(string value);
    partial void OnCityChanged();
    partial void OnStateChanging(string value);
    partial void OnStateChanged();
    partial void OnCountryChanging(string value);
    partial void OnCountryChanged();
    partial void OnShowDateChanging(System.Nullable<System.DateTime> value);
    partial void OnShowDateChanged();
    partial void OnNotesChanging(string value);
    partial void OnNotesChanged();
    partial void OnCreatedDateChanging(System.DateTime value);
    partial void OnCreatedDateChanged();
    partial void OnUpdatedDateChanging(System.Nullable<System.DateTime> value);
    partial void OnUpdatedDateChanged();
    partial void OnDeletedDateChanging(System.Nullable<System.DateTime> value);
    partial void OnDeletedDateChanged();
    partial void OnDeletedChanging(bool value);
    partial void OnDeletedChanged();
    partial void OnVenueNotesChanging(string value);
    partial void OnVenueNotesChanged();
    partial void OnPhishNetUrlChanging(string value);
    partial void OnPhishNetUrlChanged();
    #endregion
		
		public Show()
		{
			this._ListenedShows = new EntitySet<ListenedShow>(new Action<ListenedShow>(this.attach_ListenedShows), new Action<ListenedShow>(this.detach_ListenedShows));
			this._Tags = new EntitySet<Tag>(new Action<Tag>(this.attach_Tags), new Action<Tag>(this.detach_Tags));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="ShowId", Storage="_ShowId", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
		public System.Guid Id
		{
			get
			{
				return this._ShowId;
			}
			set
			{
				if ((this._ShowId != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._ShowId = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VenueName", DbType="VarChar(100)")]
		public string VenueName
		{
			get
			{
				return this._VenueName;
			}
			set
			{
				if ((this._VenueName != value))
				{
					this.OnVenueNameChanging(value);
					this.SendPropertyChanging();
					this._VenueName = value;
					this.SendPropertyChanged("VenueName");
					this.OnVenueNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_City", DbType="VarChar(50)")]
		public string City
		{
			get
			{
				return this._City;
			}
			set
			{
				if ((this._City != value))
				{
					this.OnCityChanging(value);
					this.SendPropertyChanging();
					this._City = value;
					this.SendPropertyChanged("City");
					this.OnCityChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_State", DbType="Char(2)")]
		public string State
		{
			get
			{
				return this._State;
			}
			set
			{
				if ((this._State != value))
				{
					this.OnStateChanging(value);
					this.SendPropertyChanging();
					this._State = value;
					this.SendPropertyChanged("State");
					this.OnStateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Country", DbType="NVarChar(50)")]
		public string Country
		{
			get
			{
				return this._Country;
			}
			set
			{
				if ((this._Country != value))
				{
					this.OnCountryChanging(value);
					this.SendPropertyChanging();
					this._Country = value;
					this.SendPropertyChanged("Country");
					this.OnCountryChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ShowDate", DbType="Date")]
		public System.Nullable<System.DateTime> ShowDate
		{
			get
			{
				return this._ShowDate;
			}
			set
			{
				if ((this._ShowDate != value))
				{
					this.OnShowDateChanging(value);
					this.SendPropertyChanging();
					this._ShowDate = value;
					this.SendPropertyChanged("ShowDate");
					this.OnShowDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Notes", DbType="NVarChar(MAX)")]
		public string Notes
		{
			get
			{
				return this._Notes;
			}
			set
			{
				if ((this._Notes != value))
				{
					this.OnNotesChanging(value);
					this.SendPropertyChanging();
					this._Notes = value;
					this.SendPropertyChanged("Notes");
					this.OnNotesChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreatedDate", DbType="DateTime NOT NULL")]
		public System.DateTime CreatedDate
		{
			get
			{
				return this._CreatedDate;
			}
			set
			{
				if ((this._CreatedDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._CreatedDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UpdatedDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> UpdatedDate
		{
			get
			{
				return this._UpdatedDate;
			}
			set
			{
				if ((this._UpdatedDate != value))
				{
					this.OnUpdatedDateChanging(value);
					this.SendPropertyChanging();
					this._UpdatedDate = value;
					this.SendPropertyChanged("UpdatedDate");
					this.OnUpdatedDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DeletedDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> DeletedDate
		{
			get
			{
				return this._DeletedDate;
			}
			set
			{
				if ((this._DeletedDate != value))
				{
					this.OnDeletedDateChanging(value);
					this.SendPropertyChanging();
					this._DeletedDate = value;
					this.SendPropertyChanged("DeletedDate");
					this.OnDeletedDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Deleted", DbType="Bit NOT NULL")]
		public bool Deleted
		{
			get
			{
				return this._Deleted;
			}
			set
			{
				if ((this._Deleted != value))
				{
					this.OnDeletedChanging(value);
					this.SendPropertyChanging();
					this._Deleted = value;
					this.SendPropertyChanged("Deleted");
					this.OnDeletedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VenueNotes", DbType="VarChar(MAX)")]
		public string VenueNotes
		{
			get
			{
				return this._VenueNotes;
			}
			set
			{
				if ((this._VenueNotes != value))
				{
					this.OnVenueNotesChanging(value);
					this.SendPropertyChanging();
					this._VenueNotes = value;
					this.SendPropertyChanged("VenueNotes");
					this.OnVenueNotesChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PhishNetUrl", DbType="VarChar(200)")]
		public string PhishNetUrl
		{
			get
			{
				return this._PhishNetUrl;
			}
			set
			{
				if ((this._PhishNetUrl != value))
				{
					this.OnPhishNetUrlChanging(value);
					this.SendPropertyChanging();
					this._PhishNetUrl = value;
					this.SendPropertyChanged("PhishNetUrl");
					this.OnPhishNetUrlChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Show_ListenedShow", Storage="_ListenedShows", ThisKey="Id", OtherKey="ShowId")]
		public EntitySet<ListenedShow> ListenedShows
		{
			get
			{
				return this._ListenedShows;
			}
			set
			{
				this._ListenedShows.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Show_Tag", Storage="_Tags", ThisKey="Id", OtherKey="ShowId")]
		public EntitySet<Tag> Tags
		{
			get
			{
				return this._Tags;
			}
			set
			{
				this._Tags.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_ListenedShows(ListenedShow entity)
		{
			this.SendPropertyChanging();
			entity.Show = this;
		}
		
		private void detach_ListenedShows(ListenedShow entity)
		{
			this.SendPropertyChanging();
			entity.Show = null;
		}
		
		private void attach_Tags(Tag entity)
		{
			this.SendPropertyChanging();
			entity.Show = this;
		}
		
		private void detach_Tags(Tag entity)
		{
			this.SendPropertyChanging();
			entity.Show = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.ListenedShow")]
	public partial class ListenedShow : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _Id;
		
		private System.Guid _UserId;
		
		private System.Guid _ShowId;
		
		private string _Notes;
		
		private System.Nullable<double> _Stars;
		
		private int _Status;
		
		private System.DateTime _CreatedDate;
		
		private System.Nullable<System.DateTime> _UpdatedDate;
		
		private System.Nullable<System.DateTime> _DeletedDate;
		
		private bool _Deleted;
		
		private System.DateTime _ShowDate;
		
		private EntityRef<Show> _Show;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(System.Guid value);
    partial void OnIdChanged();
    partial void OnUserIdChanging(System.Guid value);
    partial void OnUserIdChanged();
    partial void OnShowIdChanging(System.Guid value);
    partial void OnShowIdChanged();
    partial void OnNotesChanging(string value);
    partial void OnNotesChanged();
    partial void OnStarsChanging(System.Nullable<double> value);
    partial void OnStarsChanged();
    partial void OnStatusChanging(int value);
    partial void OnStatusChanged();
    partial void OnCreatedDateChanging(System.DateTime value);
    partial void OnCreatedDateChanged();
    partial void OnUpdatedDateChanging(System.Nullable<System.DateTime> value);
    partial void OnUpdatedDateChanged();
    partial void OnDeletedDateChanging(System.Nullable<System.DateTime> value);
    partial void OnDeletedDateChanged();
    partial void OnDeletedChanging(bool value);
    partial void OnDeletedChanged();
    partial void OnShowDateChanging(System.DateTime value);
    partial void OnShowDateChanged();
    #endregion
		
		public ListenedShow()
		{
			this._Show = default(EntityRef<Show>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="ListenedShowId", Storage="_Id", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
		public System.Guid Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserId", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid UserId
		{
			get
			{
				return this._UserId;
			}
			set
			{
				if ((this._UserId != value))
				{
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._UserId = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ShowId", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid ShowId
		{
			get
			{
				return this._ShowId;
			}
			set
			{
				if ((this._ShowId != value))
				{
					if (this._Show.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnShowIdChanging(value);
					this.SendPropertyChanging();
					this._ShowId = value;
					this.SendPropertyChanged("ShowId");
					this.OnShowIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Notes", DbType="VarChar(2000)")]
		public string Notes
		{
			get
			{
				return this._Notes;
			}
			set
			{
				if ((this._Notes != value))
				{
					this.OnNotesChanging(value);
					this.SendPropertyChanging();
					this._Notes = value;
					this.SendPropertyChanged("Notes");
					this.OnNotesChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Stars", DbType="Float")]
		public System.Nullable<double> Stars
		{
			get
			{
				return this._Stars;
			}
			set
			{
				if ((this._Stars != value))
				{
					this.OnStarsChanging(value);
					this.SendPropertyChanging();
					this._Stars = value;
					this.SendPropertyChanged("Stars");
					this.OnStarsChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Status", DbType="Int NOT NULL")]
		public int Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				if ((this._Status != value))
				{
					this.OnStatusChanging(value);
					this.SendPropertyChanging();
					this._Status = value;
					this.SendPropertyChanged("Status");
					this.OnStatusChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreatedDate", DbType="DateTime NOT NULL")]
		public System.DateTime CreatedDate
		{
			get
			{
				return this._CreatedDate;
			}
			set
			{
				if ((this._CreatedDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._CreatedDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UpdatedDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> UpdatedDate
		{
			get
			{
				return this._UpdatedDate;
			}
			set
			{
				if ((this._UpdatedDate != value))
				{
					this.OnUpdatedDateChanging(value);
					this.SendPropertyChanging();
					this._UpdatedDate = value;
					this.SendPropertyChanged("UpdatedDate");
					this.OnUpdatedDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DeletedDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> DeletedDate
		{
			get
			{
				return this._DeletedDate;
			}
			set
			{
				if ((this._DeletedDate != value))
				{
					this.OnDeletedDateChanging(value);
					this.SendPropertyChanging();
					this._DeletedDate = value;
					this.SendPropertyChanged("DeletedDate");
					this.OnDeletedDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Deleted", DbType="Bit NOT NULL")]
		public bool Deleted
		{
			get
			{
				return this._Deleted;
			}
			set
			{
				if ((this._Deleted != value))
				{
					this.OnDeletedChanging(value);
					this.SendPropertyChanging();
					this._Deleted = value;
					this.SendPropertyChanged("Deleted");
					this.OnDeletedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ShowDate", DbType="Date")]
		public System.DateTime ShowDate
		{
			get
			{
				return this._ShowDate;
			}
			set
			{
				if ((this._ShowDate != value))
				{
					this.OnShowDateChanging(value);
					this.SendPropertyChanging();
					this._ShowDate = value;
					this.SendPropertyChanged("ShowDate");
					this.OnShowDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Show_ListenedShow", Storage="_Show", ThisKey="ShowId", OtherKey="Id", IsForeignKey=true)]
		public Show Show
		{
			get
			{
				return this._Show.Entity;
			}
			set
			{
				Show previousValue = this._Show.Entity;
				if (((previousValue != value) 
							|| (this._Show.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Show.Entity = null;
						previousValue.ListenedShows.Remove(this);
					}
					this._Show.Entity = value;
					if ((value != null))
					{
						value.ListenedShows.Add(this);
						this._ShowId = value.Id;
					}
					else
					{
						this._ShowId = default(System.Guid);
					}
					this.SendPropertyChanged("Show");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Tag")]
	public partial class Tag : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _Id;
		
		private System.Guid _ShowId;
		
		private System.Guid _UserId;
		
		private string _Name;
		
		private System.DateTime _CreatedDate;
		
		private System.Nullable<System.DateTime> _UpdatedDate;
		
		private System.Nullable<System.DateTime> _DeletedDate;
		
		private bool _Deleted;
		
		private EntityRef<Show> _Show;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(System.Guid value);
    partial void OnIdChanged();
    partial void OnShowIdChanging(System.Guid value);
    partial void OnShowIdChanged();
    partial void OnUserIdChanging(System.Guid value);
    partial void OnUserIdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnCreatedDateChanging(System.DateTime value);
    partial void OnCreatedDateChanged();
    partial void OnUpdatedDateChanging(System.Nullable<System.DateTime> value);
    partial void OnUpdatedDateChanged();
    partial void OnDeletedDateChanging(System.Nullable<System.DateTime> value);
    partial void OnDeletedDateChanged();
    partial void OnDeletedChanging(bool value);
    partial void OnDeletedChanged();
    #endregion
		
		public Tag()
		{
			this._Show = default(EntityRef<Show>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="TagId", Storage="_Id", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
		public System.Guid Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ShowId", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid ShowId
		{
			get
			{
				return this._ShowId;
			}
			set
			{
				if ((this._ShowId != value))
				{
					if (this._Show.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnShowIdChanging(value);
					this.SendPropertyChanging();
					this._ShowId = value;
					this.SendPropertyChanged("ShowId");
					this.OnShowIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserId", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid UserId
		{
			get
			{
				return this._UserId;
			}
			set
			{
				if ((this._UserId != value))
				{
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._UserId = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreatedDate", DbType="DateTime NOT NULL")]
		public System.DateTime CreatedDate
		{
			get
			{
				return this._CreatedDate;
			}
			set
			{
				if ((this._CreatedDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._CreatedDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UpdatedDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> UpdatedDate
		{
			get
			{
				return this._UpdatedDate;
			}
			set
			{
				if ((this._UpdatedDate != value))
				{
					this.OnUpdatedDateChanging(value);
					this.SendPropertyChanging();
					this._UpdatedDate = value;
					this.SendPropertyChanged("UpdatedDate");
					this.OnUpdatedDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DeletedDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> DeletedDate
		{
			get
			{
				return this._DeletedDate;
			}
			set
			{
				if ((this._DeletedDate != value))
				{
					this.OnDeletedDateChanging(value);
					this.SendPropertyChanging();
					this._DeletedDate = value;
					this.SendPropertyChanged("DeletedDate");
					this.OnDeletedDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Deleted", DbType="Bit NOT NULL")]
		public bool Deleted
		{
			get
			{
				return this._Deleted;
			}
			set
			{
				if ((this._Deleted != value))
				{
					this.OnDeletedChanging(value);
					this.SendPropertyChanging();
					this._Deleted = value;
					this.SendPropertyChanged("Deleted");
					this.OnDeletedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Show_Tag", Storage="_Show", ThisKey="ShowId", OtherKey="Id", IsForeignKey=true)]
		public Show Show
		{
			get
			{
				return this._Show.Entity;
			}
			set
			{
				Show previousValue = this._Show.Entity;
				if (((previousValue != value) 
							|| (this._Show.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Show.Entity = null;
						previousValue.Tags.Remove(this);
					}
					this._Show.Entity = value;
					if ((value != null))
					{
						value.Tags.Add(this);
						this._ShowId = value.Id;
					}
					else
					{
						this._ShowId = default(System.Guid);
					}
					this.SendPropertyChanged("Show");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
