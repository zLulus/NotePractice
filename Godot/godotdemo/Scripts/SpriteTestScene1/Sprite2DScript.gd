extends Sprite2D


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	self.visible=true
	self.rotation=1
	self.rotation_degrees=30
	self.position=Vector2(500,500)
	self.scale= Vector2(1.25,1)
	#self.skew=10
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	var position=get_global_mouse_position()
	print(str(position.x)+","+str(position.y))
	look_at(position)
	pass
