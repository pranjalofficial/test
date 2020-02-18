using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LSSCBackEnd.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblCandidateList",
                columns: table => new
                {
                    clEnrollmentNo = table.Column<string>(maxLength: 50, nullable: false),
                    clName = table.Column<string>(maxLength: 50, nullable: false),
                    clReqNo = table.Column<int>(nullable: true),
                    clPracticalDone = table.Column<bool>(nullable: true, defaultValueSql: "((0))"),
                    clTheoryDeone = table.Column<bool>(nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCandidateList", x => x.clEnrollmentNo);
                });

            migrationBuilder.CreateTable(
                name: "tblCenter",
                columns: table => new
                {
                    centerCode = table.Column<string>(maxLength: 20, nullable: false),
                    centerName = table.Column<string>(maxLength: 40, nullable: false),
                    centerAddress = table.Column<string>(maxLength: 100, nullable: true),
                    centerContactPerson = table.Column<string>(maxLength: 30, nullable: true),
                    centerContactNo = table.Column<long>(nullable: false),
                    centerEmailID = table.Column<string>(maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCenter", x => x.centerCode);
                });

            migrationBuilder.CreateTable(
                name: "tblProject",
                columns: table => new
                {
                    projId = table.Column<string>(maxLength: 20, nullable: false),
                    projName = table.Column<string>(maxLength: 40, nullable: false),
                    projDesp = table.Column<string>(maxLength: 100, nullable: true),
                    projManager = table.Column<string>(maxLength: 30, nullable: false),
                    projAssesmentType = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProject", x => x.projId);
                });

            migrationBuilder.CreateTable(
                name: "tblQP",
                columns: table => new
                {
                    qpCode = table.Column<string>(maxLength: 100, nullable: false),
                    qpName = table.Column<string>(maxLength: 100, nullable: false),
                    qpNsqfLevel = table.Column<int>(nullable: true),
                    qpSectorName = table.Column<string>(maxLength: 20, nullable: true),
                    qpNoOfNOS = table.Column<int>(nullable: false),
                    qpMinEDLevel = table.Column<string>(maxLength: 30, nullable: true),
                    qpMaxEDLevel = table.Column<string>(maxLength: 30, nullable: true),
                    qpActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblQP", x => x.qpCode);
                });

            migrationBuilder.CreateTable(
                name: "tblTrainingPartner",
                columns: table => new
                {
                    tpCode = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    tpName = table.Column<string>(maxLength: 50, nullable: true),
                    tpActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTrainingPartner", x => x.tpCode);
                });

            migrationBuilder.CreateTable(
                name: "tblNOS",
                columns: table => new
                {
                    nosCode = table.Column<string>(maxLength: 50, nullable: false),
                    nosName = table.Column<string>(maxLength: 100, nullable: false),
                    nosActive = table.Column<bool>(nullable: false),
                    nosQpCode = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblNOS", x => x.nosCode);
                    table.ForeignKey(
                        name: "FK_nosQpConn",
                        column: x => x.nosQpCode,
                        principalTable: "tblQP",
                        principalColumn: "qpCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblQuestionBank",
                columns: table => new
                {
                    qbQuestionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    qbQuestionCode = table.Column<string>(maxLength: 12, nullable: false, computedColumnSql: "('QB'+CONVERT([nvarchar](10),[qbQuestionID]))"),
                    qbName = table.Column<string>(maxLength: 100, nullable: false),
                    qbRelatedQP = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblQuestionBank", x => x.qbQuestionCode);
                    table.ForeignKey(
                        name: "FK_qbQpConn",
                        column: x => x.qbRelatedQP,
                        principalTable: "tblQP",
                        principalColumn: "qpCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblQuestionPaperVersion",
                columns: table => new
                {
                    qvVersionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    qvVersionCode = table.Column<string>(maxLength: 12, nullable: false, computedColumnSql: "('QV'+CONVERT([nvarchar](10),[qvVersionID]))"),
                    qvName = table.Column<string>(maxLength: 50, nullable: false),
                    qvType = table.Column<string>(maxLength: 30, nullable: false),
                    qvQBCode = table.Column<string>(maxLength: 12, nullable: true),
                    qvHindi = table.Column<bool>(nullable: true),
                    qvTamil = table.Column<bool>(nullable: true),
                    qvBangla = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblQuestionPaperVersion", x => x.qvVersionCode);
                    table.ForeignKey(
                        name: "FK_QBCode",
                        column: x => x.qvQBCode,
                        principalTable: "tblQuestionBank",
                        principalColumn: "qbQuestionCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblAssessmentBatch",
                columns: table => new
                {
                    asId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    asType = table.Column<string>(maxLength: 20, nullable: false),
                    asPartner = table.Column<string>(maxLength: 40, nullable: false),
                    asState = table.Column<string>(maxLength: 30, nullable: false),
                    asCity = table.Column<string>(maxLength: 30, nullable: false),
                    asQpId = table.Column<string>(maxLength: 100, nullable: false),
                    asQuestionBankID = table.Column<string>(maxLength: 12, nullable: false),
                    asQuestionVersion = table.Column<string>(maxLength: 12, nullable: false),
                    asFacilitator = table.Column<string>(maxLength: 30, nullable: false),
                    asSDMSBatchName = table.Column<string>(maxLength: 20, nullable: false),
                    asCenterId = table.Column<string>(maxLength: 20, nullable: false),
                    asProjectId = table.Column<string>(maxLength: 20, nullable: false),
                    asContactPerson = table.Column<string>(maxLength: 40, nullable: true),
                    asContactPersonPhoneNo = table.Column<long>(nullable: true),
                    asContactPersonEmailId = table.Column<string>(maxLength: 40, nullable: true),
                    asTrainingStartDate = table.Column<DateTime>(type: "date", nullable: false),
                    asTrainingEndDate = table.Column<DateTime>(type: "date", nullable: false),
                    asAssesmentStartDate = table.Column<DateTime>(type: "date", nullable: false),
                    asAssesmentEndtDate = table.Column<DateTime>(type: "date", nullable: false),
                    asAssessmentStartTime = table.Column<TimeSpan>(nullable: true),
                    asAssessmentEndTime = table.Column<TimeSpan>(nullable: true),
                    asEnglish = table.Column<bool>(nullable: true),
                    asTamil = table.Column<bool>(nullable: true),
                    asBangla = table.Column<bool>(nullable: true),
                    asHindi = table.Column<bool>(nullable: true),
                    asRemarks = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAssessmentBatch", x => x.asId);
                    table.ForeignKey(
                        name: "FK_assecenterID",
                        column: x => x.asCenterId,
                        principalTable: "tblCenter",
                        principalColumn: "centerCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_asseprojectID",
                        column: x => x.asProjectId,
                        principalTable: "tblProject",
                        principalColumn: "projId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_asQP",
                        column: x => x.asQpId,
                        principalTable: "tblQP",
                        principalColumn: "qpCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_qbID",
                        column: x => x.asQuestionBankID,
                        principalTable: "tblQuestionBank",
                        principalColumn: "qbQuestionCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionVersion",
                        column: x => x.asQuestionVersion,
                        principalTable: "tblQuestionPaperVersion",
                        principalColumn: "qvVersionCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblPracticalQuestion",
                columns: table => new
                {
                    pqCode = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    pqNOS = table.Column<string>(maxLength: 50, nullable: false),
                    pqPC = table.Column<string>(maxLength: 100, nullable: true),
                    pqQuestion = table.Column<string>(maxLength: 500, nullable: false),
                    pqType = table.Column<string>(maxLength: 30, nullable: false),
                    pqMarks = table.Column<int>(nullable: false),
                    pqVersionOfQB = table.Column<string>(maxLength: 12, nullable: true),
                    pqLang = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPracticalQuestion", x => x.pqCode);
                    table.ForeignKey(
                        name: "FK_nosOfPQ",
                        column: x => x.pqNOS,
                        principalTable: "tblNOS",
                        principalColumn: "nosCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pqVersionID",
                        column: x => x.pqVersionOfQB,
                        principalTable: "tblQuestionPaperVersion",
                        principalColumn: "qvVersionCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblTheoryQuestions",
                columns: table => new
                {
                    tqCode = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    tqQuestion = table.Column<string>(nullable: false),
                    tqOption1 = table.Column<string>(nullable: false),
                    tqOption2 = table.Column<string>(nullable: false),
                    tqOption3 = table.Column<string>(nullable: false),
                    tqOption4 = table.Column<string>(nullable: false),
                    tqCorrectAnswer = table.Column<int>(nullable: false),
                    tqMarks = table.Column<int>(nullable: false),
                    tqVersionOfQB = table.Column<string>(maxLength: 12, nullable: true),
                    tqLanguage = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTheoryQuestions", x => x.tqCode);
                    table.ForeignKey(
                        name: "FK_versionID",
                        column: x => x.tqVersionOfQB,
                        principalTable: "tblQuestionPaperVersion",
                        principalColumn: "qvVersionCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblAssessmentProof",
                columns: table => new
                {
                    asID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    asbatchID = table.Column<int>(nullable: true),
                    asCandidateId = table.Column<string>(maxLength: 50, nullable: true),
                    asAssesser = table.Column<string>(maxLength: 30, nullable: true),
                    asPersonalPhoto = table.Column<byte[]>(nullable: true),
                    asAddharPhoto = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAssessmentProof", x => x.asID);
                    table.ForeignKey(
                        name: "FK_candidateIDas",
                        column: x => x.asCandidateId,
                        principalTable: "tblCandidateList",
                        principalColumn: "clEnrollmentNo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_batchIDas",
                        column: x => x.asbatchID,
                        principalTable: "tblAssessmentBatch",
                        principalColumn: "asId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblTheoryResult",
                columns: table => new
                {
                    trID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    trbatchID = table.Column<int>(nullable: true),
                    trCandidateId = table.Column<string>(maxLength: 50, nullable: false),
                    trMarks1 = table.Column<int>(nullable: true),
                    trMarks2 = table.Column<int>(nullable: true),
                    trMarks3 = table.Column<int>(nullable: true),
                    trMarks4 = table.Column<int>(nullable: true),
                    trMarks5 = table.Column<int>(nullable: true),
                    trMarks6 = table.Column<int>(nullable: true),
                    trMarks7 = table.Column<int>(nullable: true),
                    trMarks8 = table.Column<int>(nullable: true),
                    trMarks9 = table.Column<int>(nullable: true),
                    trMarks10 = table.Column<int>(nullable: true),
                    trMarks11 = table.Column<int>(nullable: true),
                    trMarks12 = table.Column<int>(nullable: true),
                    trMarks13 = table.Column<int>(nullable: true),
                    trMarks14 = table.Column<int>(nullable: true),
                    trMarks15 = table.Column<int>(nullable: true),
                    trMarks16 = table.Column<int>(nullable: true),
                    trMarks17 = table.Column<int>(nullable: true),
                    trMarks18 = table.Column<int>(nullable: true),
                    trMarks19 = table.Column<int>(nullable: true),
                    trMarks20 = table.Column<int>(nullable: true),
                    trTotalMarks = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTheoryResult", x => x.trID);
                    table.ForeignKey(
                        name: "FK_candidate",
                        column: x => x.trCandidateId,
                        principalTable: "tblCandidateList",
                        principalColumn: "clEnrollmentNo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_batchID",
                        column: x => x.trbatchID,
                        principalTable: "tblAssessmentBatch",
                        principalColumn: "asId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblTotalPracticalMarks",
                columns: table => new
                {
                    tpmID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    tpmbatchID = table.Column<int>(nullable: true),
                    tpmCandidateId = table.Column<string>(maxLength: 50, nullable: false),
                    tpmTotalMarks = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTotalPracticalMarks", x => x.tpmID);
                    table.ForeignKey(
                        name: "FK_candidateIDtpm",
                        column: x => x.tpmCandidateId,
                        principalTable: "tblCandidateList",
                        principalColumn: "clEnrollmentNo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_batchIDtpm",
                        column: x => x.tpmbatchID,
                        principalTable: "tblAssessmentBatch",
                        principalColumn: "asId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblPracticalResult",
                columns: table => new
                {
                    prID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    prbatchID = table.Column<int>(nullable: true),
                    prCandidateId = table.Column<string>(maxLength: 50, nullable: false),
                    prQuestionID = table.Column<int>(nullable: true),
                    prMarks = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPracticalResult", x => x.prID);
                    table.ForeignKey(
                        name: "FK_candidateIDPR",
                        column: x => x.prCandidateId,
                        principalTable: "tblCandidateList",
                        principalColumn: "clEnrollmentNo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_questionIDPR",
                        column: x => x.prQuestionID,
                        principalTable: "tblPracticalQuestion",
                        principalColumn: "pqCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_batchIDPR",
                        column: x => x.prbatchID,
                        principalTable: "tblAssessmentBatch",
                        principalColumn: "asId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFinalResult",
                columns: table => new
                {
                    frID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    frbatchID = table.Column<int>(nullable: true),
                    frCandidateId = table.Column<string>(maxLength: 50, nullable: false),
                    frTheoryID = table.Column<int>(nullable: true),
                    frPracticalID = table.Column<int>(nullable: true),
                    frTheoryResult = table.Column<int>(nullable: true),
                    frPracticalResult = table.Column<int>(nullable: true),
                    frFinalResult = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinalResult", x => x.frID);
                    table.ForeignKey(
                        name: "FK_candidateIDfr",
                        column: x => x.frCandidateId,
                        principalTable: "tblCandidateList",
                        principalColumn: "clEnrollmentNo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PracticalResultIDfr",
                        column: x => x.frPracticalID,
                        principalTable: "tblTotalPracticalMarks",
                        principalColumn: "tpmID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_theoryResultIDfr",
                        column: x => x.frTheoryID,
                        principalTable: "tblTheoryResult",
                        principalColumn: "trID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_batchIDfr",
                        column: x => x.frbatchID,
                        principalTable: "tblAssessmentBatch",
                        principalColumn: "asId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblAssessmentBatch_asCenterId",
                table: "tblAssessmentBatch",
                column: "asCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAssessmentBatch_asProjectId",
                table: "tblAssessmentBatch",
                column: "asProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAssessmentBatch_asQpId",
                table: "tblAssessmentBatch",
                column: "asQpId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAssessmentBatch_asQuestionBankID",
                table: "tblAssessmentBatch",
                column: "asQuestionBankID");

            migrationBuilder.CreateIndex(
                name: "IX_tblAssessmentBatch_asQuestionVersion",
                table: "tblAssessmentBatch",
                column: "asQuestionVersion");

            migrationBuilder.CreateIndex(
                name: "IX_tblAssessmentProof_asCandidateId",
                table: "tblAssessmentProof",
                column: "asCandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAssessmentProof_asbatchID",
                table: "tblAssessmentProof",
                column: "asbatchID");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinalResult_frCandidateId",
                table: "tblFinalResult",
                column: "frCandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinalResult_frPracticalID",
                table: "tblFinalResult",
                column: "frPracticalID");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinalResult_frTheoryID",
                table: "tblFinalResult",
                column: "frTheoryID");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinalResult_frbatchID",
                table: "tblFinalResult",
                column: "frbatchID");

            migrationBuilder.CreateIndex(
                name: "IX_tblNOS_nosQpCode",
                table: "tblNOS",
                column: "nosQpCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblPracticalQuestion_pqNOS",
                table: "tblPracticalQuestion",
                column: "pqNOS");

            migrationBuilder.CreateIndex(
                name: "IX_tblPracticalQuestion_pqVersionOfQB",
                table: "tblPracticalQuestion",
                column: "pqVersionOfQB");

            migrationBuilder.CreateIndex(
                name: "IX_tblPracticalResult_prCandidateId",
                table: "tblPracticalResult",
                column: "prCandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPracticalResult_prQuestionID",
                table: "tblPracticalResult",
                column: "prQuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_tblPracticalResult_prbatchID",
                table: "tblPracticalResult",
                column: "prbatchID");

            migrationBuilder.CreateIndex(
                name: "IX_tblQuestionBank_qbRelatedQP",
                table: "tblQuestionBank",
                column: "qbRelatedQP");

            migrationBuilder.CreateIndex(
                name: "IX_tblQuestionPaperVersion_qvQBCode",
                table: "tblQuestionPaperVersion",
                column: "qvQBCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblTheoryQuestions_tqVersionOfQB",
                table: "tblTheoryQuestions",
                column: "tqVersionOfQB");

            migrationBuilder.CreateIndex(
                name: "IX_tblTheoryResult_trCandidateId",
                table: "tblTheoryResult",
                column: "trCandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_tblTheoryResult_trbatchID",
                table: "tblTheoryResult",
                column: "trbatchID");

            migrationBuilder.CreateIndex(
                name: "IX_tblTotalPracticalMarks_tpmCandidateId",
                table: "tblTotalPracticalMarks",
                column: "tpmCandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_tblTotalPracticalMarks_tpmbatchID",
                table: "tblTotalPracticalMarks",
                column: "tpmbatchID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblAssessmentProof");

            migrationBuilder.DropTable(
                name: "tblFinalResult");

            migrationBuilder.DropTable(
                name: "tblPracticalResult");

            migrationBuilder.DropTable(
                name: "tblTheoryQuestions");

            migrationBuilder.DropTable(
                name: "tblTrainingPartner");

            migrationBuilder.DropTable(
                name: "tblTotalPracticalMarks");

            migrationBuilder.DropTable(
                name: "tblTheoryResult");

            migrationBuilder.DropTable(
                name: "tblPracticalQuestion");

            migrationBuilder.DropTable(
                name: "tblCandidateList");

            migrationBuilder.DropTable(
                name: "tblAssessmentBatch");

            migrationBuilder.DropTable(
                name: "tblNOS");

            migrationBuilder.DropTable(
                name: "tblCenter");

            migrationBuilder.DropTable(
                name: "tblProject");

            migrationBuilder.DropTable(
                name: "tblQuestionPaperVersion");

            migrationBuilder.DropTable(
                name: "tblQuestionBank");

            migrationBuilder.DropTable(
                name: "tblQP");
        }
    }
}
