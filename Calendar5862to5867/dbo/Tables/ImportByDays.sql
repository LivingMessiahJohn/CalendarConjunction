CREATE TABLE [dbo].[ImportByDays]
(
    [ImportId]         INT            NOT NULL,
    [GregorianDate]    DATE           NOT NULL,
    [YomDay]           TINYINT        NOT NULL,
    [YomNote]          NVARCHAR (200) NULL,
    [RoshKodeshDay]    BIT            NOT NULL,
    [MoonId]           INT            NOT NULL,
    [MonthDescription] NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_ImportByDays] PRIMARY KEY CLUSTERED ([ImportId] ASC, [GregorianDate] ASC),
    CONSTRAINT [CK_ImportByDays_YomDay] CHECK ([YomDay] BETWEEN 1 AND 30),
    CONSTRAINT [CK_ImportByDays_MoonId] CHECK ([MoonId] BETWEEN 0 AND 4)
);
GO
