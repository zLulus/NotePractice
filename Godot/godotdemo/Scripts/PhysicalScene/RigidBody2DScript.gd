extends RigidBody2D

var isGround:bool

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	#lock_rotation=true
	# https://docs.godotengine.org/zh-cn/4.x/classes/class_rigidbody2d.html#class-rigidbody2d-property-contact-monitor
	contact_monitor=true
	max_contacts_reported=1
	isGround=false
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass

func _physics_process(delta: float) -> void:
	var horizontal=Input.get_axis("Left","Right")
	linear_velocity=Vector2(horizontal*100,linear_velocity.y)
	if Input.is_action_just_pressed("SpaceTest"):
		if isGround:
			linear_velocity=Vector2(linear_velocity.x,-300)


func _on_body_entered(body: Node) -> void:
	print("Body entered")
	isGround=true
	pass # Replace with function body.


func _on_body_exited(body: Node) -> void:
	print("Body exited")
	isGround=false
	pass # Replace with function body.
