extends Button


@onready var progressBar:ProgressBar

func _ready() -> void:
	progressBar=get_parent().get_node("ProgressBar") as ProgressBar
	progressBar.value=Player.hp
	progressBar.max_value=Player.max_hp
	Player.hp_change.connect(on_hp_change)
	
func on_hp_change(hp,max_hp):
	progressBar.value=hp
	progressBar.max_value=max_hp

func _on_pressed() -> void:
	Player.hp=Player.hp-1
	pass # Replace with function body.
