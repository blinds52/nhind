USE [$(DBName)]

/****** Object:  Table [dbo].[Domains]    Script Date: 11/15/2010 10:33:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Domains](
	[DomainName] [varchar](255) NOT NULL,
	[AgentName] [varchar] (25) NULL,
	[DomainID] [bigint] IDENTITY(1,1) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[Status] [tinyint] NOT NULL,
 CONSTRAINT [PK_Domains] PRIMARY KEY CLUSTERED 
(
	[DomainName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [DomainID] UNIQUE NONCLUSTERED 
(
	[DomainID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DnsRecords]    Script Date: 11/15/2010 10:33:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DnsRecords](
	[RecordID] [bigint] IDENTITY(1,1) NOT NULL,
	[DomainName] [varchar](255) NOT NULL,
	[TypeID] [int] NOT NULL,
	[RecordData] [varbinary](max) NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[Notes] [varchar](500) NOT NULL,
 CONSTRAINT [PK_DnsRecords] PRIMARY KEY CLUSTERED 
(
	[RecordID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Certificates]    Script Date: 11/15/2010 10:33:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Certificates](
	[Owner] [varchar](400) NOT NULL,
	[Thumbprint] [nvarchar](64) NOT NULL,
	[CertificateID] [bigint] IDENTITY(1,1) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CertificateData] [varbinary](max) NOT NULL,
	[ValidStartDate] [datetime] NOT NULL,
	[ValidEndDate] [datetime] NOT NULL,
	[Status] [tinyint] NOT NULL,
 CONSTRAINT [PK_Certificates] PRIMARY KEY CLUSTERED 
(
	[Owner] ASC,
	[Thumbprint] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Anchors]    Script Date: 11/15/2010 10:33:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Anchors](
	[Owner] [varchar](400) NOT NULL,
	[Thumbprint] [nvarchar](64) NOT NULL,
	[CertificateID] [bigint] IDENTITY(1,1) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CertificateData] [varbinary](max) NOT NULL,
	[ValidStartDate] [datetime] NOT NULL,
	[ValidEndDate] [datetime] NOT NULL,
	[ForIncoming] [bit] NOT NULL,
	[ForOutgoing] [bit] NOT NULL,
	[Status] [tinyint] NOT NULL,
 CONSTRAINT [PK_Anchors_1] PRIMARY KEY CLUSTERED 
(
	[Owner] ASC,
	[Thumbprint] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Administrators]    Script Date: 11/15/2010 10:33:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Administrators](
	[AdministratorID] [bigint] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[PasswordHash] [varchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[Status] [tinyint] NOT NULL,
 CONSTRAINT [PK_Administrators] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Addresses]    Script Date: 11/15/2010 10:33:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Addresses](
	[EmailAddress] [varchar](400) NOT NULL,
	[AddressID] [bigint] IDENTITY(1,1) NOT NULL,
	[DomainID] [bigint] NOT NULL,
	[DisplayName] [varchar](64) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[Type] [nvarchar](64) NULL,
	[Status] [tinyint] NOT NULL,
 CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED 
(
	[EmailAddress] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[Properties]    Script Date: 01/13/2011 10:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Properties](
	[PropertyID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Value] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Properties] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Properties_ID] ON [dbo].[Properties] 
(
	[PropertyID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Blobs]    Script Date: 01/13/2011 10:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Blobs](
	[BlobID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Data] [varbinary](max) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Blobs] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Blobs_ID] ON [dbo].[Blobs] 
(
	[BlobID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO






/****** Object:  Table [dbo].[Mdns]    Script Date: 09/09/2012 22:31:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Mdns](
	[MdnIdentifier] [char](32) NOT NULL,
	[MdnId] [bigint] IDENTITY(1,1) NOT NULL,
	[MessageId] [nvarchar](255) NOT NULL,
	[RecipientAddress] [nvarchar](400) NOT NULL,
	[SenderAddress] [nvarchar](400) NOT NULL,
	[Status] [nvarchar](15) NULL,     /* Longest known disposition-type is 10 characters, 15 may provide for unknown */
	[Timedout] [bit] NOT NULL,
	[MdnProcessedDate] [datetime] NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Mdns] PRIMARY KEY CLUSTERED 
(
	[MdnIdentifier] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Index [CreateDate]    Script Date: 09/09/2012 15:03:07 ******/
CREATE NONCLUSTERED INDEX [CreateDate] ON [dbo].[Mdns] 
(
	[CreateDate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

/****** Object:  Index [MdnProcessedDate]    Script Date: 09/09/2012 22:32:42 ******/
CREATE NONCLUSTERED INDEX [MdnProcessedDate] ON [dbo].[Mdns] 
(
	[MdnProcessedDate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO









/****** Object:  Default [DF_Addresses_Status]    Script Date: 11/15/2010 10:33:24 ******/
ALTER TABLE [dbo].[Addresses] ADD  CONSTRAINT [DF_Addresses_Status]  DEFAULT ((0)) FOR [Status]
GO
/****** Object:  Default [DF_Anchors_ForIncoming]    Script Date: 11/15/2010 10:33:24 ******/
ALTER TABLE [dbo].[Anchors] ADD  CONSTRAINT [DF_Anchors_ForIncoming]  DEFAULT ((1)) FOR [ForIncoming]
GO
/****** Object:  Default [DF_Anchors_ForOutgoing]    Script Date: 11/15/2010 10:33:24 ******/
ALTER TABLE [dbo].[Anchors] ADD  CONSTRAINT [DF_Anchors_ForOutgoing]  DEFAULT ((1)) FOR [ForOutgoing]
GO
/****** Object:  Default [DF_Anchors_Status]    Script Date: 11/15/2010 10:33:24 ******/
ALTER TABLE [dbo].[Anchors] ADD  CONSTRAINT [DF_Anchors_Status]  DEFAULT ((0)) FOR [Status]
GO
/****** Object:  Default [DF_Certificates_CreateDate]    Script Date: 11/15/2010 10:33:24 ******/
ALTER TABLE [dbo].[Certificates] ADD  CONSTRAINT [DF_Certificates_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF_Certificates_Status]    Script Date: 11/15/2010 10:33:24 ******/
ALTER TABLE [dbo].[Certificates] ADD  CONSTRAINT [DF_Certificates_Status]  DEFAULT ((0)) FOR [Status]
GO
/****** Object:  Default [DF_DnsRecords_DomainName]    Script Date: 11/15/2010 10:33:24 ******/
ALTER TABLE [dbo].[DnsRecords] ADD  CONSTRAINT [DF_DnsRecords_DomainName]  DEFAULT ('') FOR [DomainName]
GO
/****** Object:  Default [DF_DnsRecords_TypeID]    Script Date: 11/15/2010 10:33:24 ******/
ALTER TABLE [dbo].[DnsRecords] ADD  CONSTRAINT [DF_DnsRecords_TypeID]  DEFAULT ((0)) FOR [TypeID]
GO
/****** Object:  Default [DF_DnsRecords_CreateDate]    Script Date: 11/15/2010 10:33:24 ******/
ALTER TABLE [dbo].[DnsRecords] ADD  CONSTRAINT [DF_DnsRecords_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF_DnsRecords_UpdateDate]    Script Date: 11/15/2010 10:33:24 ******/
ALTER TABLE [dbo].[DnsRecords] ADD  CONSTRAINT [DF_DnsRecords_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
GO
/****** Object:  Default [DF_DnsRecords_Notes]    Script Date: 11/15/2010 10:33:24 ******/
ALTER TABLE [dbo].[DnsRecords] ADD  CONSTRAINT [DF_DnsRecords_Notes]  DEFAULT ('') FOR [Notes]
GO
/****** Object:  Default [DF_Domains_Status]    Script Date: 11/15/2010 10:33:24 ******/
ALTER TABLE [dbo].[Domains] ADD  CONSTRAINT [DF_Domains_Status]  DEFAULT ((0)) FOR [Status]
GO
/****** Object:  ForeignKey [FK_Addresses_DomainID]    Script Date: 11/15/2010 10:33:24 ******/
ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_Addresses_DomainID] FOREIGN KEY([DomainID])
REFERENCES [dbo].[Domains] ([DomainID])
GO
ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_Addresses_DomainID]
GO
