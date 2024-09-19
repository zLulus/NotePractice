extends Node
class_name parent

@onready var child = $Child

func _ready():
	child.fn = print_me

func print_me():
	print(name)
	
func call_child_method():
	child.my_method()
