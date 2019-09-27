function MaskedTextBox_CreateItem(id, mask, validationExpression, autoFillMask) {
    this.Id = id;
    this.Mask = mask;
    this.ValidationExpression = validationExpression;
    this.AutoFillMask = autoFillMask;
    this.CurrentValue = document.getElementById(id).value;
}

function MaskedTextBox_FindItem(id) {
   if (eWorld_MaskedTextBox_Items == null) {
      return null;
   }
   
   for (var i=0; i<eWorld_MaskedTextBox_Items.length; i++) {
      if (eWorld_MaskedTextBox_Items[i].Id == id) {
         return eWorld_MaskedTextBox_Items[i];
      }
   }
}

function MaskedTextBox_AutoFillBox(keyCode, box, item, fromInput) {
    var addedCharacter = fromInput;
    var returnValue = true;
    
    if (item.AutoFillMask) {
    
        var nextMaskChar = MaskedTextBox_GetNextMaskCharacter(box, item, addedCharacter);
        
        while (nextMaskChar != '') {                       
            // check to see if the next mask character is an alpha/numeric character
            if (nextMaskChar.toLowerCase() == '9' || nextMaskChar.toLowerCase() == 'c' || (box.value.length == item.Mask.length)) {
                break;
            }
            
            // if the next character is not alpha or numeric, add it to the value
            var nextCharCode = nextMaskChar.toLowerCase().charCodeAt(0);            
            if ((nextCharCode != 57) && ((nextCharCode < 48) || (nextCharCode > 57)) && (nextCharCode != 99)) {
                if (!addedCharacter && keyCode != -999) {
                    box.value = box.value + String.fromCharCode(keyCode) + nextMaskChar;
                    returnValue = false;
                    addedCharacter = true;
                } else {
                    box.value = box.value + nextMaskChar;
                }
            }
            
            nextMaskChar = MaskedTextBox_GetNextMaskCharacter(box, item, addedCharacter);
        }
    }
    
    return returnValue;
}

function MaskedTextBox_GetNextMaskCharacter(box, item, addedCharacter) {
    var nextMaskChar = '';
    
    if ((box.value.length + 1) <= item.Mask.length) {
        if (box.value.length == 0) {
            nextMaskChar = item.Mask.charAt(0);
        } else {
            if (addedCharacter) {
               nextMaskChar = item.Mask.charAt(box.value.length);
            } else {
               nextMaskChar = item.Mask.charAt(box.value.length + 1);
            }
        }
    }
    
    return nextMaskChar;
}