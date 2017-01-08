--drop table LogCounts;
-- create stat table
CREATE TABLE dbo.LogCounts
	(
	LogDate date NOT NULL,
	MessageTypeId bigint NOT NULL,	
	LogCount int NOT NULL
	)  ON [PRIMARY];


ALTER TABLE dbo.LogCounts ADD CONSTRAINT DF_LogStats_LogCount DEFAULT 0 FOR LogCount;

ALTER TABLE dbo.LogCounts ADD CONSTRAINT
	PK_LogStats PRIMARY KEY CLUSTERED 
	(
	LogDate,
	MessageTypeId	
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];


-- load counts
insert into logcounts (LogDate, MessageTypeId, logCount) 
( 
	select convert(date, datetime), MessageTypeId, sum(logCount) 
	from logmessages 
	where MessageTypeId is not null
	group by convert(date, datetime), MessageTypeId		
);

