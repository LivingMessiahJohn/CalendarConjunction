CREATE TABLE [dbo].[FlattenByDates]
(
    [ImportId]         INT            NOT NULL,
    [GregorianDate]    DATE           NOT NULL,
    [YomDay]           TINYINT        NOT NULL,
    [YomNote]          NVARCHAR (200) NULL,
    [MoonId]           INT            NOT NULL,
    [MonthDescription] NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_FlattenByDates] PRIMARY KEY CLUSTERED ([GregorianDate] ASC),
    CONSTRAINT [CK_FlattenByDates_YomDay] CHECK ([YomDay] BETWEEN 1 AND 30),
    CONSTRAINT [CK_FlattenByDates_MoonId] CHECK ([MoonId] BETWEEN 0 AND 4)
);
GO
