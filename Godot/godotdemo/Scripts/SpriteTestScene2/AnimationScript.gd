extends Sprite2D

var timer:float
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	timer=timer+delta
	if timer>1:
		if self.frame>=3:
			self.frame=0
		else :
			self.frame=self.frame+1
		timer=0
	pass
