function MaskedTextBox_FocusMask(box, id) {
    var item = MaskedTextBox_FindItem(id);
    
    if (box.value == '' && item.AutoFillMask) {
        MaskedTextBox_AutoFillBox(-999, box, item, false);

        // attempt to move the cursor at the end of the input characters
        if (box.createTextRange()) {
            var range = box.createTextRange();
            
            range.moveStart('character', box.value.length);
            range.collapse();
            range.select();
        }
    }
}

function MaskedTextBox_VerifyMask(e, box, id) {
    var item = MaskedTextBox_FindItem(id);
    var keyCode = e.keyCode;
    
    if (box.value == '' && item.AutoFillMask && item.Mask.length != 0 && keyCode != 13 && keyCode != 8 && keyCode != 0) {
      MaskedTextBox_AutoFillBox(-999, box, item, false);
    }
    
    var currentValue = box.value;
    var returnValue = false;
    
    if (item.Mask.length == 0 || keyCode == 13 || keyCode == 8 || keyCode == 0) {
        returnValue = true;
    } else {
        // attempt to clear the currently selected text
        if (document.selection != null) {
           if (document.selection.createRange().text != '') {
               document.selection.clear();
           }
        }
   
        var maskChar = item.Mask.substr(currentValue.length, 1);
        var nextMaskChar = '';
        
        if ((currentValue.length + 1) <= item.Mask.length) {
            nextMaskChar = item.Mask.substr(currentValue.length + 1, 1);
        }
 
        // determine if what was typeed is the valid next mask character       
        if ((maskChar.charCodeAt(0) == 57) && (keyCode >= 48) && (keyCode <= 57)) { // digit
            returnValue = true;
        } else if ((maskChar.toLowerCase().charCodeAt(0) == 99) && (((keyCode >= 65) && (keyCode <=90)) || ((keyCode >= 97) && (keyCode <= 122)))) { // character
            returnValue = true;
        } else if (maskChar == String.fromCharCode(keyCode)) { // special
            returnValue = true;
        } else { // no match
            returnValue = false;
        }
        
        if (returnValue) {
            if (box.value.length == 0) {
               box.value += String.fromCharCode(keyCode);
               returnValue = MaskedTextBox_AutoFillBox(keyCode, box, item, true);
               returnValue = false;
            } else {
               returnValue = MaskedTextBox_AutoFillBox(keyCode, box, item, false);
            }
        }
    }
    
    return returnValue;
}

function MaskedTextBox_Paste(box, id) {
    var item = MaskedTextBox_FindItem(id);
    var returnValue = false;
    
    if (item.ValidationExpression.length > 0) {
        var pasteData = window.clipboardData.getData("Text");
        eval("var re = /" + item.ValidationExpression + "/;");
        if (re.test(pasteData) || pasteData.length == 0) {
            returnValue = true;
        }
    }
    
    return returnValue;
}