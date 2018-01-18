
Create table Account
(
AccountNumber int Primary Key,
Balance decimal(18,2),
LastUpdatedon DateTime
)

insert into account values(10001,300,GETUTCDATE())
insert into account values(10002,400,GETUTCDATE())



Create Table AccountHistory
(
AccountId int PRIMARY KEY IDENTITY,
AccountNumber int,
Currency VARCHAR(5),
TransactionType VARCHAR(20),
Amount decimal(18,4),
LastUpdateOn DateTime
)

insert into AccountHistory(AccountNumber,Currency,TransactionType,Amount,LastUpdateOn) values(10001,'USD','Deposit',100,GETUTCDATE())
insert into AccountHistory(AccountNumber,Currency,TransactionType,Amount,LastUpdateOn) values(10001,'USD','Deposit',100,GETUTCDATE())
insert into AccountHistory(AccountNumber,Currency,TransactionType,Amount,LastUpdateOn) values(10001,'USD','Deposit',100,GETUTCDATE())
insert into AccountHistory(AccountNumber,Currency,TransactionType,Amount,LastUpdateOn) values(10002,'USD','Deposit',200,GETUTCDATE())
insert into AccountHistory(AccountNumber,Currency,TransactionType,Amount,LastUpdateOn) values(10002,'USD','Deposit',200,GETUTCDATE())

Create Table CurrencyConversion
(
CurrencyConversionID int PRIMARY KEY IDENTITY,
APICurrencyDim int,
ExpectedCurrency varchar(5),
CurrencyValue decimal(18,4),
APICurrencyType varchar(5)
)



INSERT INTO CurrencyConversion(APICurrencyDim,ExpectedCurrency,CurrencyValue,APICurrencyType) VALUES(1,'INR',0.0156979,'USD')
INSERT INTO CurrencyConversion(APICurrencyDim,ExpectedCurrency,CurrencyValue,APICurrencyType) VALUES(1,'TBH',0.0312376,'USD')
INSERT INTO CurrencyConversion(APICurrencyDim,ExpectedCurrency,CurrencyValue,APICurrencyType) VALUES(1,'GDB',1.34814,'USD')
INSERT INTO CurrencyConversion(APICurrencyDim,ExpectedCurrency,CurrencyValue,APICurrencyType) VALUES(1,'USD',1,'USD')


INSERT INTO CurrencyConversion(APICurrencyDim,ExpectedCurrency,CurrencyValue,APICurrencyType) VALUES(1,'TBH',1.98970,'INR')
INSERT INTO CurrencyConversion(APICurrencyDim,ExpectedCurrency,CurrencyValue,APICurrencyType) VALUES(1,'GDB',85.8991,'INR')
INSERT INTO CurrencyConversion(APICurrencyDim,ExpectedCurrency,CurrencyValue,APICurrencyType) VALUES(1,'USD',63.6872,'INR')
INSERT INTO CurrencyConversion(APICurrencyDim,ExpectedCurrency,CurrencyValue,APICurrencyType) VALUES(1,'INR',1,'INR')


INSERT INTO CurrencyConversion(APICurrencyDim,ExpectedCurrency,CurrencyValue,APICurrencyType) VALUES(1,'INR',0.502692,'TBH')
INSERT INTO CurrencyConversion(APICurrencyDim,ExpectedCurrency,CurrencyValue,APICurrencyType) VALUES(1,'GDB',43.1739,'TBH')
INSERT INTO CurrencyConversion(APICurrencyDim,ExpectedCurrency,CurrencyValue,APICurrencyType) VALUES(1,'USD',32.0084,'TBH')
INSERT INTO CurrencyConversion(APICurrencyDim,ExpectedCurrency,CurrencyValue,APICurrencyType) VALUES(1,'TBH',1,'TBH')


INSERT INTO CurrencyConversion(APICurrencyDim,ExpectedCurrency,CurrencyValue,APICurrencyType) VALUES(1,'INR',0.0116445,'GDB')
INSERT INTO CurrencyConversion(APICurrencyDim,ExpectedCurrency,CurrencyValue,APICurrencyType) VALUES(1,'THB',0.0231675,'GDB')
INSERT INTO CurrencyConversion(APICurrencyDim,ExpectedCurrency,CurrencyValue,APICurrencyType) VALUES(1,'USD',0.741520,'GDB')
INSERT INTO CurrencyConversion(APICurrencyDim,ExpectedCurrency,CurrencyValue,APICurrencyType) VALUES(1,'GDB',1,'GDB')


