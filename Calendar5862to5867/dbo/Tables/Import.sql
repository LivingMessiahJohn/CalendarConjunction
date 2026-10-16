CREATE TABLE [dbo].[Import]
(
    [Id]           INT            NOT NULL,
    [MonthHeading] NVARCHAR (100) NULL,
    [Yom1]         NVARCHAR (200) NULL,
    [Yom2]         NVARCHAR (200) NULL,
    [Yom3]         NVARCHAR (200) NULL,
    [Yom4]         NVARCHAR (200) NULL,
    [Yom5]         NVARCHAR (200) NULL,
    [Yom6]         NVARCHAR (200) NULL,
    [Yom7]         NVARCHAR (200) NULL,
    CONSTRAINT [PK_Import] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
