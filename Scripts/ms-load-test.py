
from locust import HttpUser, task, constant
import random
from faker import Faker
fake = Faker()

class QuickstartUser(HttpUser):
	wait_time = constant(0)
	host = "http://38.242.135.17:22415"

	ids : list[str] = []
 
	@task(50)
	def test_get_method(self):
		if (self.ids.__len__() > 0):
			self.client.get("/User/api/user/" + random.choice(self.ids))

	@task(50)
	def test_post_method(self):
		response = self.client.post("/User/api/user", {
			"FirstName" : fake.first_name(),
			"LastName" : fake.last_name(),
			"Mail" : fake.email(),
		})
		if (response.ok): 
			print(response.text)
			json_response_dict = response.json()
			request_id = json_response_dict['Id']
			self.ids.append(request_id)

	@task(50)
	def test_deletet_method(self):
		if (self.ids.__len__() > 0):
			id = random.choice(self.ids)
			self.ids.remove(id)
			self.client.get("/User/api/user/" + id)
		
	def on_start(self):
		pass