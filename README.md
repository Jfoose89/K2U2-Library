# K2U2-Library SQL Project
Overview

This project implements a small library system with books, members, loans, and automated tracking of available copies.
The database enforces loan rules at the database level to ensure that books cannot be loaned when no copies are available, while preserving historical loan data.

Database Structure

Book
Stores books with:

CopiesTotal

CopiesAvailable

Member
Stores library members.

Loan
Tracks which member has borrowed which book, including:

LoanDate

DueDate

ReturnDate (NULL = active loan)

LoanLog
Logs loan and return actions for auditing purposes.

Stored Procedure & Views
usp_RegisterLoan

All new loans are created using the usp_RegisterLoan stored procedure.

Verifies that CopiesAvailable > 0 before inserting a loan

Inserts a loan with ReturnDate = NULL

Throws an error and rolls back the transaction if no copies are available

This guarantees that invalid loans cannot be created.

Active Loans View

vw_ActiveLoans

Defines active loans as records where ReturnDate IS NULL

Used for reporting and verification

Ensures active loans are clearly separated from historical data

Triggers

Triggers are used to enforce data integrity even if inserts or updates bypass the stored procedure.

trg_DecreaseCopiesOnLoan
Decreases CopiesAvailable when a new active loan is inserted.

trg_IncreaseCopiesOnReturn
Increases CopiesAvailable when a loan is returned (ReturnDate is set).

trg_LogLoanActions
Logs all loan and return actions into LoanLog.

Triggers ensure that availability cannot become negative and that all actions are logged.

Inventory Consistency

Before testing, book availability is synchronized using the following logic:

CopiesAvailable = CopiesTotal - ActiveLoans


This guarantees a consistent baseline state, even when historical loans exist in the database.

SQL Scripts / Files

create_tables_and_inserts.sql
Creates the database, tables, sample books, members, and initial loan data.

stored_procedure.sql
Creates usp_RegisterLoan.

triggers.sql
Creates all triggers (decrease, increase, logging).

backfill_loanlog.sql (optional, one-time)
Populates LoanLog for pre-existing loans.

verify_inventory.sql (optional)
Verifies that:

CopiesAvailable + ActiveLoans = CopiesTotal


test_loans.sql (recommended)
Executes controlled test cases to validate loan behavior and error handling.

Usage

Run create_tables_and_inserts.sql

Run stored_procedure.sql

Run triggers.sql

(Optional) Run backfill_loanlog.sql

(Optional) Run verify_inventory.sql

Run test_loans.sql to validate behavior

Result

Books cannot be loaned if no copies are available

Availability remains accurate at all times

Historical loan data is preserved

Business rules are enforced at the database level

All key scenarios are tested and verified


ER Diagram

ENTITIES:

BOOK
(Attributes)
- BookID (PK)
- ISBN (UNIQUE)
- Title
- Author
- PublishedYear
- CopiesTotal
- CopiesAvailable

MEMBER
(Attributes)
- MemberID (PK)
- FirstName
- LastName
- Email (UNIQUE)
- Phone

LOAN
(Attributes)
- LoanID (PK)
- BookID (FK → BOOK.BookID)
- MemberID (FK → MEMBER.MemberID)
- LoanDate
- DueDate
- ReturnedDate
