extends RayCast2D


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	# not necessary
	# force_raycast_update()
	if self.is_colliding():
		var area=self.get_collider() as Area2D
		if area!=null:
			print("Collision:"+area.name)
	else:
		print("No Collision")
	pass
