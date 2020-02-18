IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [tblCandidateList] (
    [clEnrollmentNo] nvarchar(50) NOT NULL,
    [clName] nvarchar(50) NOT NULL,
    [clReqNo] int NULL,
    [clPracticalDone] bit NULL DEFAULT (((0))),
    [clTheoryDeone] bit NULL DEFAULT (((0))),
    CONSTRAINT [PK_tblCandidateList] PRIMARY KEY ([clEnrollmentNo])
);

GO

CREATE TABLE [tblCenter] (
    [centerCode] nvarchar(20) NOT NULL,
    [centerName] nvarchar(40) NOT NULL,
    [centerAddress] nvarchar(100) NULL,
    [centerContactPerson] nvarchar(30) NULL,
    [centerContactNo] bigint NOT NULL,
    [centerEmailID] nvarchar(40) NULL,
    CONSTRAINT [PK_tblCenter] PRIMARY KEY ([centerCode])
);

GO

CREATE TABLE [tblProject] (
    [projId] nvarchar(20) NOT NULL,
    [projName] nvarchar(40) NOT NULL,
    [projDesp] nvarchar(100) NULL,
    [projManager] nvarchar(30) NOT NULL,
    [projAssesmentType] nvarchar(20) NULL,
    CONSTRAINT [PK_tblProject] PRIMARY KEY ([projId])
);

GO

CREATE TABLE [tblQP] (
    [qpCode] nvarchar(100) NOT NULL,
    [qpName] nvarchar(100) NOT NULL,
    [qpNsqfLevel] int NULL,
    [qpSectorName] nvarchar(20) NULL,
    [qpNoOfNOS] int NOT NULL,
    [qpMinEDLevel] nvarchar(30) NULL,
    [qpMaxEDLevel] nvarchar(30) NULL,
    [qpActive] bit NOT NULL,
    CONSTRAINT [PK_tblQP] PRIMARY KEY ([qpCode])
);

GO

CREATE TABLE [tblTrainingPartner] (
    [tpCode] int NOT NULL IDENTITY,
    [tpName] nvarchar(50) NULL,
    [tpActive] bit NOT NULL,
    CONSTRAINT [PK_tblTrainingPartner] PRIMARY KEY ([tpCode])
);

GO

CREATE TABLE [tblNOS] (
    [nosCode] nvarchar(50) NOT NULL,
    [nosName] nvarchar(100) NOT NULL,
    [nosActive] bit NOT NULL,
    [nosQpCode] nvarchar(100) NULL,
    CONSTRAINT [PK_tblNOS] PRIMARY KEY ([nosCode]),
    CONSTRAINT [FK_nosQpConn] FOREIGN KEY ([nosQpCode]) REFERENCES [tblQP] ([qpCode]) ON DELETE NO ACTION
);

GO

CREATE TABLE [tblQuestionBank] (
    [qbQuestionID] int NOT NULL IDENTITY,
    [qbQuestionCode] AS ('QB'+CONVERT([nvarchar](10),[qbQuestionID])),
    [qbName] nvarchar(100) NOT NULL,
    [qbRelatedQP] nvarchar(100) NULL,
    CONSTRAINT [PK_tblQuestionBank] PRIMARY KEY ([qbQuestionCode]),
    CONSTRAINT [FK_qbQpConn] FOREIGN KEY ([qbRelatedQP]) REFERENCES [tblQP] ([qpCode]) ON DELETE NO ACTION
);

GO

CREATE TABLE [tblQuestionPaperVersion] (
    [qvVersionID] int NOT NULL IDENTITY,
    [qvVersionCode] AS ('QV'+CONVERT([nvarchar](10),[qvVersionID])),
    [qvName] nvarchar(50) NOT NULL,
    [qvType] nvarchar(30) NOT NULL,
    [qvQBCode] nvarchar(12) NULL,
    [qvHindi] bit NULL,
    [qvTamil] bit NULL,
    [qvBangla] bit NULL,
    CONSTRAINT [PK_tblQuestionPaperVersion] PRIMARY KEY ([qvVersionCode]),
    CONSTRAINT [FK_QBCode] FOREIGN KEY ([qvQBCode]) REFERENCES [tblQuestionBank] ([qbQuestionCode]) ON DELETE NO ACTION
);

GO

CREATE TABLE [tblAssessmentBatch] (
    [asId] int NOT NULL IDENTITY,
    [asType] nvarchar(20) NOT NULL,
    [asPartner] nvarchar(40) NOT NULL,
    [asState] nvarchar(30) NOT NULL,
    [asCity] nvarchar(30) NOT NULL,
    [asQpId] nvarchar(100) NOT NULL,
    [asQuestionBankID] nvarchar(12) NOT NULL,
    [asQuestionVersion] nvarchar(12) NOT NULL,
    [asFacilitator] nvarchar(30) NOT NULL,
    [asSDMSBatchName] nvarchar(20) NOT NULL,
    [asCenterId] nvarchar(20) NOT NULL,
    [asProjectId] nvarchar(20) NOT NULL,
    [asContactPerson] nvarchar(40) NULL,
    [asContactPersonPhoneNo] bigint NULL,
    [asContactPersonEmailId] nvarchar(40) NULL,
    [asTrainingStartDate] date NOT NULL,
    [asTrainingEndDate] date NOT NULL,
    [asAssesmentStartDate] date NOT NULL,
    [asAssesmentEndtDate] date NOT NULL,
    [asAssessmentStartTime] time NULL,
    [asAssessmentEndTime] time NULL,
    [asEnglish] bit NULL,
    [asTamil] bit NULL,
    [asBangla] bit NULL,
    [asHindi] bit NULL,
    [asRemarks] nvarchar(100) NULL,
    CONSTRAINT [PK_tblAssessmentBatch] PRIMARY KEY ([asId]),
    CONSTRAINT [FK_assecenterID] FOREIGN KEY ([asCenterId]) REFERENCES [tblCenter] ([centerCode]) ON DELETE NO ACTION,
    CONSTRAINT [FK_asseprojectID] FOREIGN KEY ([asProjectId]) REFERENCES [tblProject] ([projId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_asQP] FOREIGN KEY ([asQpId]) REFERENCES [tblQP] ([qpCode]) ON DELETE NO ACTION,
    CONSTRAINT [FK_qbID] FOREIGN KEY ([asQuestionBankID]) REFERENCES [tblQuestionBank] ([qbQuestionCode]) ON DELETE NO ACTION,
    CONSTRAINT [FK_QuestionVersion] FOREIGN KEY ([asQuestionVersion]) REFERENCES [tblQuestionPaperVersion] ([qvVersionCode]) ON DELETE NO ACTION
);

GO

CREATE TABLE [tblPracticalQuestion] (
    [pqCode] int NOT NULL IDENTITY,
    [pqNOS] nvarchar(50) NOT NULL,
    [pqPC] nvarchar(100) NULL,
    [pqQuestion] nvarchar(500) NOT NULL,
    [pqType] nvarchar(30) NOT NULL,
    [pqMarks] int NOT NULL,
    [pqVersionOfQB] nvarchar(12) NULL,
    [pqLang] nvarchar(50) NULL,
    CONSTRAINT [PK_tblPracticalQuestion] PRIMARY KEY ([pqCode]),
    CONSTRAINT [FK_nosOfPQ] FOREIGN KEY ([pqNOS]) REFERENCES [tblNOS] ([nosCode]) ON DELETE NO ACTION,
    CONSTRAINT [FK_pqVersionID] FOREIGN KEY ([pqVersionOfQB]) REFERENCES [tblQuestionPaperVersion] ([qvVersionCode]) ON DELETE NO ACTION
);

GO

CREATE TABLE [tblTheoryQuestions] (
    [tqCode] int NOT NULL IDENTITY,
    [tqQuestion] nvarchar(max) NOT NULL,
    [tqOption1] nvarchar(max) NOT NULL,
    [tqOption2] nvarchar(max) NOT NULL,
    [tqOption3] nvarchar(max) NOT NULL,
    [tqOption4] nvarchar(max) NOT NULL,
    [tqCorrectAnswer] int NOT NULL,
    [tqMarks] int NOT NULL,
    [tqVersionOfQB] nvarchar(12) NULL,
    [tqLanguage] nvarchar(50) NULL,
    CONSTRAINT [PK_tblTheoryQuestions] PRIMARY KEY ([tqCode]),
    CONSTRAINT [FK_versionID] FOREIGN KEY ([tqVersionOfQB]) REFERENCES [tblQuestionPaperVersion] ([qvVersionCode]) ON DELETE NO ACTION
);

GO

CREATE TABLE [tblAssessmentProof] (
    [asID] int NOT NULL IDENTITY,
    [asbatchID] int NULL,
    [asCandidateId] nvarchar(50) NULL,
    [asAssesser] nvarchar(30) NULL,
    [asPersonalPhoto] varbinary(max) NULL,
    [asAddharPhoto] varbinary(max) NULL,
    CONSTRAINT [PK_tblAssessmentProof] PRIMARY KEY ([asID]),
    CONSTRAINT [FK_candidateIDas] FOREIGN KEY ([asCandidateId]) REFERENCES [tblCandidateList] ([clEnrollmentNo]) ON DELETE NO ACTION,
    CONSTRAINT [FK_batchIDas] FOREIGN KEY ([asbatchID]) REFERENCES [tblAssessmentBatch] ([asId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [tblTheoryResult] (
    [trID] int NOT NULL IDENTITY,
    [trbatchID] int NULL,
    [trCandidateId] nvarchar(50) NOT NULL,
    [trMarks1] int NULL,
    [trMarks2] int NULL,
    [trMarks3] int NULL,
    [trMarks4] int NULL,
    [trMarks5] int NULL,
    [trMarks6] int NULL,
    [trMarks7] int NULL,
    [trMarks8] int NULL,
    [trMarks9] int NULL,
    [trMarks10] int NULL,
    [trMarks11] int NULL,
    [trMarks12] int NULL,
    [trMarks13] int NULL,
    [trMarks14] int NULL,
    [trMarks15] int NULL,
    [trMarks16] int NULL,
    [trMarks17] int NULL,
    [trMarks18] int NULL,
    [trMarks19] int NULL,
    [trMarks20] int NULL,
    [trTotalMarks] int NULL,
    CONSTRAINT [PK_tblTheoryResult] PRIMARY KEY ([trID]),
    CONSTRAINT [FK_candidate] FOREIGN KEY ([trCandidateId]) REFERENCES [tblCandidateList] ([clEnrollmentNo]) ON DELETE NO ACTION,
    CONSTRAINT [FK_batchID] FOREIGN KEY ([trbatchID]) REFERENCES [tblAssessmentBatch] ([asId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [tblTotalPracticalMarks] (
    [tpmID] int NOT NULL IDENTITY,
    [tpmbatchID] int NULL,
    [tpmCandidateId] nvarchar(50) NOT NULL,
    [tpmTotalMarks] int NOT NULL,
    CONSTRAINT [PK_tblTotalPracticalMarks] PRIMARY KEY ([tpmID]),
    CONSTRAINT [FK_candidateIDtpm] FOREIGN KEY ([tpmCandidateId]) REFERENCES [tblCandidateList] ([clEnrollmentNo]) ON DELETE NO ACTION,
    CONSTRAINT [FK_batchIDtpm] FOREIGN KEY ([tpmbatchID]) REFERENCES [tblAssessmentBatch] ([asId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [tblPracticalResult] (
    [prID] int NOT NULL IDENTITY,
    [prbatchID] int NULL,
    [prCandidateId] nvarchar(50) NOT NULL,
    [prQuestionID] int NULL,
    [prMarks] int NULL,
    CONSTRAINT [PK_tblPracticalResult] PRIMARY KEY ([prID]),
    CONSTRAINT [FK_candidateIDPR] FOREIGN KEY ([prCandidateId]) REFERENCES [tblCandidateList] ([clEnrollmentNo]) ON DELETE NO ACTION,
    CONSTRAINT [FK_questionIDPR] FOREIGN KEY ([prQuestionID]) REFERENCES [tblPracticalQuestion] ([pqCode]) ON DELETE NO ACTION,
    CONSTRAINT [FK_batchIDPR] FOREIGN KEY ([prbatchID]) REFERENCES [tblAssessmentBatch] ([asId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [tblFinalResult] (
    [frID] int NOT NULL IDENTITY,
    [frbatchID] int NULL,
    [frCandidateId] nvarchar(50) NOT NULL,
    [frTheoryID] int NULL,
    [frPracticalID] int NULL,
    [frTheoryResult] int NULL,
    [frPracticalResult] int NULL,
    [frFinalResult] int NULL,
    CONSTRAINT [PK_tblFinalResult] PRIMARY KEY ([frID]),
    CONSTRAINT [FK_candidateIDfr] FOREIGN KEY ([frCandidateId]) REFERENCES [tblCandidateList] ([clEnrollmentNo]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PracticalResultIDfr] FOREIGN KEY ([frPracticalID]) REFERENCES [tblTotalPracticalMarks] ([tpmID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_theoryResultIDfr] FOREIGN KEY ([frTheoryID]) REFERENCES [tblTheoryResult] ([trID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_batchIDfr] FOREIGN KEY ([frbatchID]) REFERENCES [tblAssessmentBatch] ([asId]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_tblAssessmentBatch_asCenterId] ON [tblAssessmentBatch] ([asCenterId]);

GO

CREATE INDEX [IX_tblAssessmentBatch_asProjectId] ON [tblAssessmentBatch] ([asProjectId]);

GO

CREATE INDEX [IX_tblAssessmentBatch_asQpId] ON [tblAssessmentBatch] ([asQpId]);

GO

CREATE INDEX [IX_tblAssessmentBatch_asQuestionBankID] ON [tblAssessmentBatch] ([asQuestionBankID]);

GO

CREATE INDEX [IX_tblAssessmentBatch_asQuestionVersion] ON [tblAssessmentBatch] ([asQuestionVersion]);

GO

CREATE INDEX [IX_tblAssessmentProof_asCandidateId] ON [tblAssessmentProof] ([asCandidateId]);

GO

CREATE INDEX [IX_tblAssessmentProof_asbatchID] ON [tblAssessmentProof] ([asbatchID]);

GO

CREATE INDEX [IX_tblFinalResult_frCandidateId] ON [tblFinalResult] ([frCandidateId]);

GO

CREATE INDEX [IX_tblFinalResult_frPracticalID] ON [tblFinalResult] ([frPracticalID]);

GO

CREATE INDEX [IX_tblFinalResult_frTheoryID] ON [tblFinalResult] ([frTheoryID]);

GO

CREATE INDEX [IX_tblFinalResult_frbatchID] ON [tblFinalResult] ([frbatchID]);

GO

CREATE INDEX [IX_tblNOS_nosQpCode] ON [tblNOS] ([nosQpCode]);

GO

CREATE INDEX [IX_tblPracticalQuestion_pqNOS] ON [tblPracticalQuestion] ([pqNOS]);

GO

CREATE INDEX [IX_tblPracticalQuestion_pqVersionOfQB] ON [tblPracticalQuestion] ([pqVersionOfQB]);

GO

CREATE INDEX [IX_tblPracticalResult_prCandidateId] ON [tblPracticalResult] ([prCandidateId]);

GO

CREATE INDEX [IX_tblPracticalResult_prQuestionID] ON [tblPracticalResult] ([prQuestionID]);

GO

CREATE INDEX [IX_tblPracticalResult_prbatchID] ON [tblPracticalResult] ([prbatchID]);

GO

CREATE INDEX [IX_tblQuestionBank_qbRelatedQP] ON [tblQuestionBank] ([qbRelatedQP]);

GO

CREATE INDEX [IX_tblQuestionPaperVersion_qvQBCode] ON [tblQuestionPaperVersion] ([qvQBCode]);

GO

CREATE INDEX [IX_tblTheoryQuestions_tqVersionOfQB] ON [tblTheoryQuestions] ([tqVersionOfQB]);

GO

CREATE INDEX [IX_tblTheoryResult_trCandidateId] ON [tblTheoryResult] ([trCandidateId]);

GO

CREATE INDEX [IX_tblTheoryResult_trbatchID] ON [tblTheoryResult] ([trbatchID]);

GO

CREATE INDEX [IX_tblTotalPracticalMarks_tpmCandidateId] ON [tblTotalPracticalMarks] ([tpmCandidateId]);

GO

CREATE INDEX [IX_tblTotalPracticalMarks_tpmbatchID] ON [tblTotalPracticalMarks] ([tpmbatchID]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200217065335_MyFirstMigration', N'2.1.14-servicing-32113');

GO

