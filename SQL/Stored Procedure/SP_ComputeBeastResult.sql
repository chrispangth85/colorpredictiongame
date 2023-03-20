USE ColorPrediction
GO

/****** Object:  StoredProcedure [dbo].[SP_ComputeBeastResult]    Script Date: 6/3/2023 8:41:44 PM ******/
DROP PROCEDURE [dbo].[SP_ComputeBeastResult]
GO

/****** Object:  StoredProcedure [dbo].[SP_ComputeBeastResult]    Script Date: 6/3/2023 8:41:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



--Runs on every 00:00:31 (last 30 seconds we do not allow member place bet, thus we calc result here]
CREATE PROCEDURE [dbo].[SP_ComputeBeastResult]
AS
	SET NOCOUNT ON

	DECLARE @gameSessionID INT = 0
	DECLARE @gameID INT = 0
	
	--First is to deactivate the previous game that already passed
	DECLARE tzCur10 CURSOR FOR
			SELECT [CGAMESES_ID], [CGAME_ID]
			FROM [dbo].[CVD_GAME_SESSION]
			WHERE [CGAME_STATUS] = 1
			AND [CGAME_RESULT] = -1
	OPEN tzCur10
	FETCH NEXT FROM tzCur10 INTO @gameSessionID, @gameID
	WHILE @@FETCH_STATUS = 0
		BEGIN 	

		DECLARE @totalBetReturn0 DECIMAL(15,2) = 0, @totalBetReturn1 DECIMAL(15,2) = 0, @totalBetReturn2 DECIMAL(15,2) = 0, @totalBetReturn3 DECIMAL(15,2) = 0,
		@totalBetReturn4 DECIMAL(15,2) = 0, @totalBetReturn5 DECIMAL(15,2) = 0, @totalBetReturn6 DECIMAL(15,2) = 0, @totalBetReturn7 DECIMAL(15,2) = 0, 
		@totalBetReturn8 DECIMAL(15,2) = 0, @totalBetReturn9 DECIMAL(15,2) = 0, @totalBetReturnGreen DECIMAL(15,2) = 0, 
		@totalBetReturnViolet DECIMAL(15,2) = 0, @totalBetReturnRed DECIMAL(15,2) = 0, @totalBetReturnRedZero DECIMAL(15,2) = 0,
		@totalBetReturnGreenFive DECIMAL(15,2) = 0

		DECLARE @exists INT = 0

		SELECT @exists = 1,
		@totalBetReturn0 = [CGAME_NO0_RETURN], @totalBetReturn1 = [CGAME_NO1_RETURN], @totalBetReturn2 = [CGAME_NO2_RETURN], 
		@totalBetReturn3 = [CGAME_NO3_RETURN], @totalBetReturn4 = [CGAME_NO4_RETURN], @totalBetReturn5 = [CGAME_NO5_RETURN],
		@totalBetReturn6 = [CGAME_NO6_RETURN], @totalBetReturn7 = [CGAME_NO7_RETURN], @totalBetReturn8 = [CGAME_NO8_RETURN],
		@totalBetReturn9 = [CGAME_NO9_RETURN], @totalBetReturnGreen = [CGAME_GREEN_RETURN], @totalBetReturnViolet = [CGAME_VIOLET_RETURN],
		@totalBetReturnRed = [CGAME_RED_RETURN], @totalBetReturnRedZero = CGAME_RED0_RETURN, @totalBetReturnGreenFive = CGAME_GREEN5_RETURN
		FROM [dbo].[CVD_GAME]
		WHERE [CGAME_TYPE] = 1--Beast mode
		AND [CGAME_ID] = @gameID
		
		--if it is beast mode > we compute the result from here
		IF @exists = 1
		BEGIN
			DECLARE @totalIncome DECIMAL(15,2) = 0
			DECLARE @totalBet0 DECIMAL(15,2) = 0, @totalBet1 DECIMAL(15,2) = 0, @totalBet2 DECIMAL(15,2) = 0, @totalBet3 DECIMAL(15,2) = 0,
			@totalBet4 DECIMAL(15,2) = 0, @totalBet5 DECIMAL(15,2) = 0, @totalBet6 DECIMAL(15,2) = 0, @totalBet7 DECIMAL(15,2) = 0, 
			@totalBet8 DECIMAL(15,2) = 0, @totalBet9 DECIMAL(15,2) = 0, @totalBetGreen DECIMAL(15,2) = 0, 
			@totalBetViolet DECIMAL(15,2) = 0, @totalBetRed DECIMAL(15,2) = 0

			DECLARE @totalPayout0 DECIMAL(15,2) = 0, @totalPayout1 DECIMAL(15,2) = 0, @totalPayout2 DECIMAL(15,2) = 0, @totalPayout3 DECIMAL(15,2) = 0,
			@totalPayout4 DECIMAL(15,2) = 0, @totalPayout5 DECIMAL(15,2) = 0, @totalPayout6 DECIMAL(15,2) = 0, @totalPayout7 DECIMAL(15,2) = 0,
			@totalPayout8 DECIMAL(15,2) = 0, @totalPayout9 DECIMAL(15,2) = 0
			
			DECLARE @numz INT
			DECLARE @sumz DECIMAL(15,2)

			--Get the total bet for 0-9 + Green + Violet + Red
			DECLARE tzCur1011 CURSOR FOR
					SELECT [CGAME_NUMBER] AS Numz, SUM([CGAME_AMOUNT]) AS SUMz
					FROM [dbo].[CVD_GAME_SESSION_BET]
					WHERE [CGAMESES_ID] = @gameSessionID
					GROUP BY [CGAME_NUMBER]
			OPEN tzCur1011
			FETCH NEXT FROM tzCur1011 INTO @numz, @sumz
			WHILE @@FETCH_STATUS = 0
				BEGIN 	
				
				IF @numz = 0
				BEGIN
					SET @totalBet0 = @sumz
					SET @totalIncome = @totalIncome + @sumz
				END
				ELSE IF @numz = 1
				BEGIN
					SET @totalBet1 = @sumz
					SET @totalIncome = @totalIncome + @sumz
				END
				ELSE IF @numz = 2
				BEGIN
					SET @totalBet2 = @sumz
					SET @totalIncome = @totalIncome + @sumz
				END
				ELSE IF @numz = 3
				BEGIN
					SET @totalBet3 = @sumz
					SET @totalIncome = @totalIncome + @sumz
				END
				ELSE IF @numz = 4
				BEGIN
					SET @totalBet4 = @sumz
					SET @totalIncome = @totalIncome + @sumz
				END
				ELSE IF @numz = 5
				BEGIN
					SET @totalBet5 = @sumz
					SET @totalIncome = @totalIncome + @sumz
				END
				ELSE IF @numz = 6
				BEGIN
					SET @totalBet6 = @sumz
					SET @totalIncome = @totalIncome + @sumz
				END
				ELSE IF @numz = 7
				BEGIN
					SET @totalBet7 = @sumz
					SET @totalIncome = @totalIncome + @sumz
				END
				ELSE IF @numz = 8
				BEGIN
					SET @totalBet8 = @sumz
					SET @totalIncome = @totalIncome + @sumz
				END
				ELSE IF @numz = 9
				BEGIN
					SET @totalBet9 = @sumz
					SET @totalIncome = @totalIncome + @sumz
				END
				ELSE IF @numz = 55
				BEGIN
					SET @totalBetGreen = @sumz
					SET @totalIncome = @totalIncome + @sumz
				END
				ELSE IF @numz = 66
				BEGIN
					SET @totalBetViolet = @sumz
					SET @totalIncome = @totalIncome + @sumz
				END
				ELSE IF @numz = 77
				BEGIN
					SET @totalBetRed = @sumz
					SET @totalIncome = @totalIncome + @sumz
				END

			FETCH NEXT FROM tzCur1011 INTO @numz, @sumz
			END
			CLOSE tzCur1011
			DEALLOCATE tzCur1011

			--Compute the total payout for each category
			SET @totalPayout0 = (@totalBet0 * @totalBetReturn0) + (@totalBetViolet * @totalBetReturnViolet) + (@totalBetRed * @totalBetReturnRedZero)
			SET @totalPayout1 = (@totalBet1 * @totalBetReturn1) + (@totalBetGreen * @totalBetReturnGreen)
			SET @totalPayout2 = @totalBet2 * @totalBetReturn2 + (@totalBetRed * @totalBetReturnRed)
			SET @totalPayout3 = @totalBet3 * @totalBetReturn3 + (@totalBetGreen * @totalBetReturnGreen)
			SET @totalPayout4 = @totalBet4 * @totalBetReturn4 + (@totalBetRed * @totalBetReturnRed)
			SET @totalPayout5 = @totalBet5 * @totalBetReturn5 + (@totalBetViolet * @totalBetReturnViolet) + (@totalBetGreen * @totalBetReturnGreenFive)
			SET @totalPayout6 = @totalBet6 * @totalBetReturn6 + (@totalBetRed * @totalBetReturnRed)
			SET @totalPayout7 = @totalBet7 * @totalBetReturn7 + (@totalBetGreen * @totalBetReturnGreen)
			SET @totalPayout8 = @totalBet8 * @totalBetReturn8 + (@totalBetRed * @totalBetReturnRed)
			SET @totalPayout9 = @totalBet9 * @totalBetReturn9 + (@totalBetGreen * @totalBetReturnGreen)

			--PRINT @totalIncome
			--PRINT @totalPayout0
			--PRINT @totalPayout1
			--PRINT @totalPayout2
			--PRINT @totalPayout3
			--PRINT @totalPayout4
			--PRINT @totalPayout5
			--PRINT @totalPayout6
			--PRINT @totalPayout7
			--PRINT @totalPayout8
			--PRINT @totalPayout9

			--Create temporary table to put in all the number who have lesser payout than income
			CREATE TABLE #TempTableNumber (Num INT);
			
			--Insert those payout which are lesser than income
			IF (@totalPayout0 = 0 OR @totalPayout0 < @totalIncome)
			BEGIN
				INSERT INTO #TempTableNumber (Num)
				VALUES (0);
			END
			IF (@totalPayout1 = 0 OR @totalPayout1 < @totalIncome)
			BEGIN
				INSERT INTO #TempTableNumber (Num)
				VALUES (1);
			END
			IF (@totalPayout2 = 0 OR @totalPayout2 < @totalIncome)
			BEGIN
				INSERT INTO #TempTableNumber (Num)
				VALUES (2);
			END
			IF (@totalPayout3 = 0 OR @totalPayout3 < @totalIncome)
			BEGIN
				INSERT INTO #TempTableNumber (Num)
				VALUES (3);
			END
			IF (@totalPayout4 = 0 OR @totalPayout4 < @totalIncome)
			BEGIN
				INSERT INTO #TempTableNumber (Num)
				VALUES (4);
			END
			IF (@totalPayout5 = 0 OR @totalPayout5 < @totalIncome)
			BEGIN
				INSERT INTO #TempTableNumber (Num)
				VALUES (5);
			END
			IF (@totalPayout6 = 0 OR @totalPayout6 < @totalIncome)
			BEGIN
				INSERT INTO #TempTableNumber (Num)
				VALUES (6);
			END
			IF (@totalPayout7 = 0 OR @totalPayout7 < @totalIncome)
			BEGIN
				INSERT INTO #TempTableNumber (Num)
				VALUES (7);
			END
			IF (@totalPayout8 = 0 OR @totalPayout8 < @totalIncome)
			BEGIN
				INSERT INTO #TempTableNumber (Num)
				VALUES (8);
			END
			IF (@totalPayout9 = 0 OR @totalPayout9 < @totalIncome)
			BEGIN
				INSERT INTO #TempTableNumber (Num)
				VALUES (9);
			END

			DECLARE @winnerNumber INT = 0

			SELECT TOP 1 @winnerNumber = Num
			FROM #TempTableNumber
			ORDER BY NEWID();

			PRINT @winnerNumber

			UPDATE [dbo].[CVD_GAME_SESSION]
			SET CGAME_RESULT = @winnerNumber
			WHERE [CGAMESES_ID] = @gameSessionID

			DROP TABLE #TempTableNumber;
		END

	FETCH NEXT FROM tzCur10 INTO @gameSessionID, @gameID
	END
	CLOSE tzCur10
	DEALLOCATE tzCur10

	RETURN
GO


