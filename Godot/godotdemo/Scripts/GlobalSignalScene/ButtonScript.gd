extends Button

var number:int

@onready var label

func _ready() -> void:
	number=0
	label=get_parent().get_node("Label")
	Player.UpdateMoney.connect(UpdateMoneyForButton)
	
func UpdateMoneyForButton(money:int):
	label.text=str(money)

func _on_pressed() -> void:
	Player.Money=Player.Money+1
	pass # Replace with function body.
