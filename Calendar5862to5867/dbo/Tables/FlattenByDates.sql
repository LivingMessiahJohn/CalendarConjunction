CREATE TABLE [dbo].[FlattenByDates]
(
    [ImportId]         INT            NOT NULL,
    [GregorianDate]    DATE           NOT NULL,
    [YomDay]           TINYINT        NOT NULL,
    [YomNote]          NVARCHAR (200) NULL,
    [RoshKodeshErev]   BIT            NOT NULL,
    [RoshKodeshDay]    BIT            NOT NULL,
    [MoonHalfWaning]   BIT            NOT NULL,
    [MoonHalfWaxing]   BIT            NOT NULL,
    [MoonFull]         BIT            NOT NULL,
    [MonthDescription] NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_FlattenByDates] PRIMARY KEY CLUSTERED ([GregorianDate] ASC, [ImportId] ASC),
    CONSTRAINT [CK_FlattenByDates_YomDay] CHECK ([YomDay] BETWEEN 1 AND 30)
);
GO
