drop table FilterSources;
drop table FilterTypes;
drop table filters;

CREATE TABLE dbo.Filters
(
	Id bigint NOT NULL IDENTITY (1, 1),
	Name nvarchar(100) NOT NULL,
	Distribution varchar(MAX) NULL,
	Start datetime null,
	[End]  datetime null,
	Span int not null default 0,
	Limit int not null default 0
)  ON [PRIMARY];

ALTER TABLE dbo.Filters ADD CONSTRAINT	PK_Filters PRIMARY KEY CLUSTERED 	(Id) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];

CREATE TABLE dbo.FilterSources
(
	FilterId bigint NOT NULL,
	SourceId bigint NOT NULL
)  ON [PRIMARY];
ALTER TABLE dbo.FilterSources ADD CONSTRAINT	PK_FilterSources PRIMARY KEY CLUSTERED 	(FilterId,SourceId) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];
ALTER TABLE dbo.FilterSources ADD CONSTRAINT	FK_FilterSources_Filters FOREIGN KEY	(FilterId) REFERENCES dbo.Filters	(Id) ON UPDATE  NO ACTION 	 ON DELETE  CASCADE ;	

CREATE TABLE dbo.FilterTypes
(
	FilterId bigint NOT NULL,
	TypeId bigint NOT NULL
)  ON [PRIMARY];
ALTER TABLE dbo.FilterTypes ADD CONSTRAINT	PK_FilterTypes PRIMARY KEY CLUSTERED (FilterId,	TypeId) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];
ALTER TABLE dbo.FilterTypes ADD CONSTRAINT	FK_FilterTypes_Filters FOREIGN KEY	(FilterId) REFERENCES dbo.Filters	(Id) ON UPDATE  NO ACTION 	 ON DELETE  CASCADE ;


--CREATE TABLE dbo.FilterTypes
--(
--	FilterId bigint NOT NULL,
--	TypeId bigint NOT NULL
--)  ON [PRIMARY];
--ALTER TABLE dbo.FilterTypes ADD CONSTRAINT	PK_FilterTypes PRIMARY KEY CLUSTERED (FilterId,	TypeId) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];
--ALTER TABLE dbo.FilterTypes ADD CONSTRAINT	FK_FilterTypes_Filters FOREIGN KEY	(FilterId) REFERENCES dbo.Filters	(Id) ON UPDATE  NO ACTION 	 ON DELETE  CASCADE ;
