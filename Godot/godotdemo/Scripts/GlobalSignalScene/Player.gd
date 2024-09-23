extends RefCounted

var Money:int:
	set(value):
		Money=value
		emit_signal("UpdateMoney",value)

signal UpdateMoney(money:int)
