IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250501192409_InitialCreate'
)
BEGIN
    CREATE TABLE [Contacts] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [PhoneNumber] nvarchar(max) NOT NULL,
        [OptInTime] datetime2 NOT NULL,
        [OptOutTime] datetime2 NOT NULL,
        [LastActiveTime] datetime2 NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_Contacts] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250501192409_InitialCreate'
)
BEGIN
    CREATE TABLE [Questions] (
        [Id] int NOT NULL IDENTITY,
        [QuestionNumber] int NOT NULL,
        [Text] nvarchar(max) NOT NULL,
        [IsRequired] bit NOT NULL,
        [OrderInSurvey] int NOT NULL,
        [Type] int NOT NULL,
        CONSTRAINT [PK_Questions] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250501192409_InitialCreate'
)
BEGIN
    CREATE TABLE [SurveyResponses] (
        [Id] int NOT NULL IDENTITY,
        [SurveyId] int NOT NULL,
        [ContactId] int NOT NULL,
        [StartedAt] datetime2 NOT NULL,
        [CompletedAt] datetime2 NULL,
        CONSTRAINT [PK_SurveyResponses] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250501192409_InitialCreate'
)
BEGIN
    CREATE TABLE [Messages] (
        [Id] int NOT NULL IDENTITY,
        [PhoneNumber] nvarchar(max) NOT NULL,
        [Content] nvarchar(max) NOT NULL,
        [Url] nvarchar(max) NULL,
        [SentAt] datetime2 NOT NULL,
        [ContactId] int NULL,
        [DeliveredAt] datetime2 NULL,
        [ErrorMessage] nvarchar(max) NULL,
        CONSTRAINT [PK_Messages] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Messages_Contacts_ContactId] FOREIGN KEY ([ContactId]) REFERENCES [Contacts] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250501192409_InitialCreate'
)
BEGIN
    CREATE TABLE [Surveys] (
        [Id] int NOT NULL IDENTITY,
        [StartedAt] datetime2 NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [ContactId] int NOT NULL,
        CONSTRAINT [PK_Surveys] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Surveys_Contacts_ContactId] FOREIGN KEY ([ContactId]) REFERENCES [Contacts] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250501192409_InitialCreate'
)
BEGIN
    CREATE TABLE [Answers] (
        [Id] int NOT NULL IDENTITY,
        [Response] nvarchar(max) NOT NULL,
        [SubmittedAt] datetime2 NOT NULL,
        [QuestionId] int NOT NULL,
        [SurveyId] int NOT NULL,
        [ContactId] int NOT NULL,
        [SurveyResponseId] int NULL,
        CONSTRAINT [PK_Answers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Answers_Questions_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [Questions] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Answers_SurveyResponses_SurveyResponseId] FOREIGN KEY ([SurveyResponseId]) REFERENCES [SurveyResponses] ([Id]),
        CONSTRAINT [FK_Answers_Surveys_SurveyId] FOREIGN KEY ([SurveyId]) REFERENCES [Surveys] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250501192409_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IsActive', N'LastActiveTime', N'Name', N'OptInTime', N'OptOutTime', N'PhoneNumber') AND [object_id] = OBJECT_ID(N'[Contacts]'))
        SET IDENTITY_INSERT [Contacts] ON;
    EXEC(N'INSERT INTO [Contacts] ([Id], [IsActive], [LastActiveTime], [Name], [OptInTime], [OptOutTime], [PhoneNumber])
    VALUES (1, CAST(1 AS bit), ''2025-05-01T19:24:09.0630370Z'', N''Demo User'', ''2025-01-01T00:00:00.0000000'', ''0001-01-01T00:00:00.0000000'', N''+1234567890'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IsActive', N'LastActiveTime', N'Name', N'OptInTime', N'OptOutTime', N'PhoneNumber') AND [object_id] = OBJECT_ID(N'[Contacts]'))
        SET IDENTITY_INSERT [Contacts] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250501192409_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IsRequired', N'OrderInSurvey', N'QuestionNumber', N'Text', N'Type') AND [object_id] = OBJECT_ID(N'[Questions]'))
        SET IDENTITY_INSERT [Questions] ON;
    EXEC(N'INSERT INTO [Questions] ([Id], [IsRequired], [OrderInSurvey], [QuestionNumber], [Text], [Type])
    VALUES (1, CAST(1 AS bit), 1, 1, N''What is your thoughts on the demo so far? (1 being awful and 10 being perfect)'', 2),
    (2, CAST(1 AS bit), 2, 2, N''Would you recommend this demo to a friend or family member?'', 0),
    (3, CAST(0 AS bit), 3, 3, N''What do you like so far about the demo?'', 1),
    (4, CAST(0 AS bit), 4, 4, N''Any thoughts or suggestions?'', 3)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IsRequired', N'OrderInSurvey', N'QuestionNumber', N'Text', N'Type') AND [object_id] = OBJECT_ID(N'[Questions]'))
        SET IDENTITY_INSERT [Questions] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250501192409_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ContactId', N'CreatedAt', N'StartedAt') AND [object_id] = OBJECT_ID(N'[Surveys]'))
        SET IDENTITY_INSERT [Surveys] ON;
    EXEC(N'INSERT INTO [Surveys] ([Id], [ContactId], [CreatedAt], [StartedAt])
    VALUES (1, 1, ''2025-01-01T00:00:00.0000000'', ''2025-05-01T19:24:09.0632970Z'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ContactId', N'CreatedAt', N'StartedAt') AND [object_id] = OBJECT_ID(N'[Surveys]'))
        SET IDENTITY_INSERT [Surveys] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250501192409_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ContactId', N'QuestionId', N'Response', N'SubmittedAt', N'SurveyId', N'SurveyResponseId') AND [object_id] = OBJECT_ID(N'[Answers]'))
        SET IDENTITY_INSERT [Answers] ON;
    EXEC(N'INSERT INTO [Answers] ([Id], [ContactId], [QuestionId], [Response], [SubmittedAt], [SurveyId], [SurveyResponseId])
    VALUES (1, 1, 3, N''The Text Messages'', ''2025-01-01T00:00:00.0000000'', 1, NULL),
    (2, 1, 3, N''The iMessage like preview'', ''2025-01-01T00:00:00.0000000'', 1, NULL),
    (3, 1, 3, N''The Slack Opt-in Page'', ''2025-01-01T00:00:00.0000000'', 1, NULL),
    (4, 1, 3, N''Ease of Use'', ''2025-01-01T00:00:00.0000000'', 1, NULL),
    (5, 1, 3, N''The Survey itself'', ''2025-01-01T00:00:00.0000000'', 1, NULL),
    (6, 1, 3, N''N/A'', ''2025-01-01T00:00:00.0000000'', 1, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ContactId', N'QuestionId', N'Response', N'SubmittedAt', N'SurveyId', N'SurveyResponseId') AND [object_id] = OBJECT_ID(N'[Answers]'))
        SET IDENTITY_INSERT [Answers] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250501192409_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Answers_QuestionId] ON [Answers] ([QuestionId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250501192409_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Answers_SurveyId] ON [Answers] ([SurveyId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250501192409_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Answers_SurveyResponseId] ON [Answers] ([SurveyResponseId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250501192409_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Messages_ContactId] ON [Messages] ([ContactId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250501192409_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Surveys_ContactId] ON [Surveys] ([ContactId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250501192409_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250501192409_InitialCreate', N'9.0.4');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250502145320_UpdateSurveyTemplateName'
)
BEGIN
    ALTER TABLE [Answers] DROP CONSTRAINT [FK_Answers_Surveys_SurveyId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250502145320_UpdateSurveyTemplateName'
)
BEGIN
    ALTER TABLE [Surveys] DROP CONSTRAINT [FK_Surveys_Contacts_ContactId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250502145320_UpdateSurveyTemplateName'
)
BEGIN
    ALTER TABLE [Surveys] DROP CONSTRAINT [PK_Surveys];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250502145320_UpdateSurveyTemplateName'
)
BEGIN
    EXEC sp_rename N'[Surveys]', N'SurveyTemplates', 'OBJECT';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250502145320_UpdateSurveyTemplateName'
)
BEGIN
    EXEC sp_rename N'[SurveyTemplates].[IX_Surveys_ContactId]', N'IX_SurveyTemplates_ContactId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250502145320_UpdateSurveyTemplateName'
)
BEGIN
    ALTER TABLE [SurveyTemplates] ADD CONSTRAINT [PK_SurveyTemplates] PRIMARY KEY ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250502145320_UpdateSurveyTemplateName'
)
BEGIN
    EXEC(N'UPDATE [Contacts] SET [LastActiveTime] = ''2025-05-02T14:53:20.0304940Z''
    WHERE [Id] = 1;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250502145320_UpdateSurveyTemplateName'
)
BEGIN
    EXEC(N'UPDATE [SurveyTemplates] SET [StartedAt] = ''2025-05-02T14:53:20.0311750Z''
    WHERE [Id] = 1;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250502145320_UpdateSurveyTemplateName'
)
BEGIN
    ALTER TABLE [Answers] ADD CONSTRAINT [FK_Answers_SurveyTemplates_SurveyId] FOREIGN KEY ([SurveyId]) REFERENCES [SurveyTemplates] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250502145320_UpdateSurveyTemplateName'
)
BEGIN
    ALTER TABLE [SurveyTemplates] ADD CONSTRAINT [FK_SurveyTemplates_Contacts_ContactId] FOREIGN KEY ([ContactId]) REFERENCES [Contacts] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250502145320_UpdateSurveyTemplateName'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250502145320_UpdateSurveyTemplateName', N'9.0.4');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250503131853_ChangedSurveyTemplate'
)
BEGIN
    ALTER TABLE [SurveyTemplates] DROP CONSTRAINT [FK_SurveyTemplates_Contacts_ContactId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250503131853_ChangedSurveyTemplate'
)
BEGIN
    DECLARE @var sysname;
    SELECT @var = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SurveyTemplates]') AND [c].[name] = N'StartedAt');
    IF @var IS NOT NULL EXEC(N'ALTER TABLE [SurveyTemplates] DROP CONSTRAINT [' + @var + '];');
    ALTER TABLE [SurveyTemplates] DROP COLUMN [StartedAt];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250503131853_ChangedSurveyTemplate'
)
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SurveyTemplates]') AND [c].[name] = N'ContactId');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [SurveyTemplates] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [SurveyTemplates] ALTER COLUMN [ContactId] int NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250503131853_ChangedSurveyTemplate'
)
BEGIN
    ALTER TABLE [SurveyTemplates] ADD [SurveyName] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250503131853_ChangedSurveyTemplate'
)
BEGIN
    ALTER TABLE [Questions] ADD [SurveyTemplateId] int NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250503131853_ChangedSurveyTemplate'
)
BEGIN
    EXEC(N'UPDATE [Contacts] SET [LastActiveTime] = ''2025-05-03T13:18:52.4208400Z''
    WHERE [Id] = 1;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250503131853_ChangedSurveyTemplate'
)
BEGIN
    EXEC(N'UPDATE [Questions] SET [SurveyTemplateId] = NULL
    WHERE [Id] = 1;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250503131853_ChangedSurveyTemplate'
)
BEGIN
    EXEC(N'UPDATE [Questions] SET [SurveyTemplateId] = NULL
    WHERE [Id] = 2;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250503131853_ChangedSurveyTemplate'
)
BEGIN
    EXEC(N'UPDATE [Questions] SET [SurveyTemplateId] = NULL
    WHERE [Id] = 3;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250503131853_ChangedSurveyTemplate'
)
BEGIN
    EXEC(N'UPDATE [Questions] SET [SurveyTemplateId] = NULL
    WHERE [Id] = 4;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250503131853_ChangedSurveyTemplate'
)
BEGIN
    EXEC(N'UPDATE [SurveyTemplates] SET [ContactId] = NULL, [SurveyName] = N''TextDemo''
    WHERE [Id] = 1;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250503131853_ChangedSurveyTemplate'
)
BEGIN
    CREATE INDEX [IX_Questions_SurveyTemplateId] ON [Questions] ([SurveyTemplateId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250503131853_ChangedSurveyTemplate'
)
BEGIN
    ALTER TABLE [Questions] ADD CONSTRAINT [FK_Questions_SurveyTemplates_SurveyTemplateId] FOREIGN KEY ([SurveyTemplateId]) REFERENCES [SurveyTemplates] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250503131853_ChangedSurveyTemplate'
)
BEGIN
    ALTER TABLE [SurveyTemplates] ADD CONSTRAINT [FK_SurveyTemplates_Contacts_ContactId] FOREIGN KEY ([ContactId]) REFERENCES [Contacts] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250503131853_ChangedSurveyTemplate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250503131853_ChangedSurveyTemplate', N'9.0.4');
END;

COMMIT;
GO

