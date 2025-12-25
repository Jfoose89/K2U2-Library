# K2U2-Library SQL Project

Overview

This project implements a small library system with books, members, loans, and automated tracking of copies available. The database ensures data integrity for loans and returns and logs all actions for auditing.

Database Structure

Book: Stores books with total and available copies.

Member: Stores library members.

Loan: Tracks which member has borrowed which book and when; includes ReturnDate.

LoanLog: Logs all loan and return actions.

Triggers & Stored Procedure

usp_RegisterLoan:

Each loan insertion checks that copies are available before inserting.

When a loan is created, the available copy count is reduced.

Returned loans store a ReturnDate and restore availability.

Active loans are identified via ReturnDate IS NULL and exposed through a view (vw_ActiveLoans).

Triggers:

trg_DecreaseCopiesOnLoan: Decreases CopiesAvailable when a new loan is inserted.

trg_IncreaseCopiesOnReturn: Increases CopiesAvailable when a loan is returned.

trg_LogLoanActions: Logs all loan and return actions into LoanLog.

SQL Scripts / Files

triggers.sql – All triggers (Decrease, Increase, Log).

backfill_loanlog.sql – Populates LoanLog for existing loans (one-time setup).

verify_inventory.sql – Optional: Checks that CopiesAvailable + active loans = CopiesTotal for all books.

stored_procedure.sql – usp_RegisterLoan stored procedure to safely register loans.

create_tables_and_inserts.sql – Database and table creation, sample books and members, initial loan inserts.

Usage

Run create_tables_and_inserts.sql to set up the database.

Run stored_procedure.sql to create the loan procedure.

Run triggers.sql to create triggers.

(Optional) Run backfill_loanlog.sql to populate the log for pre-existing loans.

(Optional) Run verify_inventory.sql to confirm consistency.
