import { shallowMount } from "@vue/test-utils";
import HelloWorld from "@/components/HelloWorld.vue";
import Vehicle from "@/components/Vehicle.vue";

// describe("HelloWorld.vue", () => {
//   it("renders props.msg when passed", () => {
//     const msg = "HelloWorld";
//     const wrapper = shallowMount(HelloWorld, {
//       propsData: { msg }
//     });
//     expect(wrapper.text()).toMatch(msg);
//   });
// });

describe("vehicle.vue", () => {
  it("renders Vehicle title", () => {
    const wrapper = shallowMount(Vehicle);
    expect(wrapper.find("h4").text()).toMatch("Vehicle List");
  });
});
