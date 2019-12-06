--delete from timetable
DECLARE  @InStartDate DATE;
DECLARE  @InStopDate DATE;
DECLARE  @tm FLOAT;
DECLARE  @curDayOfWeek INT;

SET @InStartDate='20190101';
SET @InStopDate='20191231';

WHILE(@InSTartDate <= @InStopDate)
  BEGIN
  SET @tm='0';
  SET @curDayOfWeek = DATEPART(dw, @InSTartDate);
  IF(@curDayOfWeek >= '1' AND @curDayOfWeek <= '4')
		SET @tm='7';
	ELSE IF (@curDayOfWeek = '5')
		SET @tm='6.5';
   INSERT INTO TimeTable(date, planningtime) values (@InSTartDate, @tm);
  SELECT @InStartDate=DATEADD(DD,1,@InStartDate);
  END;