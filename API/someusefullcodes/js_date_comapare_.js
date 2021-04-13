public isDateValid(DateOne){
      try
      {
        var returnValue = false;

        let dateTwo = '22/10/2021' 
        dateTwo = this.ConvertToDateObject(dateTwo);

        if (this.IsValidDate(DateOne) && this.IsValidDate(dateTwo))
        {
          let dateTwoUTCTimeStamp = this.GetUTCTimeStamp(dateTwo);
          let dateOneUTCTimeStamp = this.GetUTCTimeStamp(DateOne);

          if (dateOneUTCTimeStamp >= dateTwoUTCTimeStamp)
            returnValue = true;

        }
		
		return returnValue;
      }
      catch (err) { }
    }





private IsValidDate(date)
      {
        if (date != null && date != undefined)
        {
          try
          {
            let convertToDateObject = new Date(date);
            if (Object.prototype.toString.call(convertToDateObject) === "[object Date]" && !isNaN(convertToDateObject.getTime()) && convertToDateObject.getTime() > 0)
              return true;
          }
          catch (err)
          {
            return false;
          }

        }

        return false;

      }
	
	
	
	