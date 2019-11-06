import { shallowMount, mount } from "@vue/test-utils";
import HelloWorld from "@/components/HelloWorld.vue";
import VehicleList from "@/components/VehicleList.vue";
import flushPromises from "flush-promises";
import axios from "axios";

jest.mock("axios", () => ({
  get: jest.fn(() => Promise.resolve({ data: 3 }))
}));

afterEach(() => {
  jest.resetModules();
  jest.clearAllMocks();
});

describe("vehicle.vue", () => {
  it("renders Vehicle title", () => {
    const wrapper = mount(VehicleList);
    expect(wrapper.find("h4").text()).toMatch("Vehicle List");
  });

  it("get Customer list from BackEnd and use SignalR if on local host", () => {
    const wrapper = mount(VehicleList);
    if (process.env.VUE_APP_SignalR_ENABLED == "true") {
      expect(axios.get).toBeCalledTimes(2);
      console.log("Called twice: customer list and SignalR enabled");
    } else {
      expect(axios.get).toBeCalledTimes(1);
      console.log("Called once for customer list, SignalR disabled");
    }
  });
});
