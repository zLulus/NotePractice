extends Control

var max_hp:int=100
var hp:int=100:
	set(value):
		hp=value
		emit_signal("hp_change",hp,max_hp)

signal hp_change(hp:int,max_hp:int)
