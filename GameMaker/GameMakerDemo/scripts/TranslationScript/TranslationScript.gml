// v2.3.0的脚本资产已更改，请参见\ n // https://help.yoyogames.com/hc/en-us/articles/360005277377
function Translation(key){
	if (!is_string(key)) {  
        show_error("Translation(key) expects a string as parameter.",0);  
        return ""; 
    }  
	if(key=="TestButtonKey")
	{
		return "Test Button"
	}
	if(key=="GoToRoomButtonKey")
	{
		return "Go To Room Button";
	}
	if(key=="ShowMenuDialogButtonKey")
	{
		return "Show Menu";
	}
	if(key=="CloseMenuDialogButtonKey")
	{
		return "Close Menu";
	}
	if(key=="DatabaseButtonKey")
	{
		return "Database Test Button";
	}
	return "Can not get value from Translation method.";
}